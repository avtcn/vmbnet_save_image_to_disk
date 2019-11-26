using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AVT.VmbAPINET;
using System.IO;

namespace CameraViewer1
{
   
    public partial class Form1 : Form
    {
        private Vimba m_Vimba = null;
        private AVT_Cam Camera1;
        private string Cur_CameraID = null;
        double Cam1_ExpTime = 1500;
        string Cam1_TriggerSource = "Freerun";

        bool Cur_camIsOpen = false;
        bool Cur_camIsAcq = false;

        double maxRate = 0;
        string defaultPath = "";
        string ImgSavePath = "E:\\image_saves_200GB\\";

        bool sendSoftTrigger = false;

        struct lemon
        {
            public Image MyImg;
            public string Timestamp;
            public ulong nFrameIndex;
            public VmbFrameStatusType status;
        };
        Queue<lemon> ImageSavequeue1 = new Queue<lemon>();


        object lockImagequeue = new object();
        object lockImagecount = new object();
        object lockSavemode = new object();

        int imgcount = 0;
        bool SaveModeOn = false;

        int frameCount = 0;
        int saveCount = 0;

        List<string> cameraList = new List<string>();

        public Form1()
        {
            InitializeComponent();

            tb_SavePath.Text = ImgSavePath;

        }
        private void LogMessage(string message)
        {
            if (null == message)
            {
                throw new ArgumentNullException("message");
            }
             tB_Log.AppendText(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}: {1}", DateTime.Now, message)+"\r\n");          
        }
      
        //Add an error log message and show an error message box
        private void LogError(string message)
        {
            LogMessage(message);

            MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private bool Init_Camera()
        {
            
            try
            {
                Camera1 = new AVT_Cam();
                // Camera1.m_Cam  = m_Vimba.GetCameraByID("192.168.0.45");//两种方式都可以
                Cur_CameraID = m_Vimba.Cameras[0].Id;
                if (Cur_CameraID != null)
                {
                    Camera1.m_Cam = m_Vimba.GetCameraByID(Cur_CameraID);
                    bT_OpenCamera.Enabled = true;
                    LogMessage("相机初始化成功");
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                bT_OpenCamera.Enabled = false;
                LogMessage("相机初始化失败");
                return false;
            }
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            try 
            {
                Vimba vimba = new Vimba();
                vimba.Startup();
                m_Vimba = vimba;
                //m_Vimba.OnCameraListChanged += OnCameraListChange;

                /*
                try
                {
                    //UpdateCameraList();
                }
                catch (Exception exception)
                {
                    LogError("Could not update camera list. Reason: " + exception.Message);
                }
                */
                Init_Camera();
                // updateControls();

            }
            catch (Exception exception)
            {
                LogError("Could not startup Vimba API. Reason: " + exception.Message);
            }
        }
        private void OnCameraListChange(VmbUpdateTriggerType reason)
        {
            switch (reason)
            {
                case VmbUpdateTriggerType.VmbUpdateTriggerPluggedIn:
                case VmbUpdateTriggerType.VmbUpdateTriggerPluggedOut:
                    {
                        UpdateCameraList();
                    }
                    break;

                default:
                    break;
            }
        }
        private void UpdateCameraList()
        {
            //Remember the old selection (if there was any)y
            string oldSelectedItem = null;
            List_Cam.Items.Clear();

            cameraList.Clear();
            CameraCollection cameras = m_Vimba.Cameras;
            for (int i = 0; i < cameras.Count; i++)
            {               
                cameraList.Add(cameras[i].Id);
            }

            string newSelectedItem = null;
            foreach (string cameraID in cameraList)
            {
                List_Cam.Items.Add(cameraID);

                if (null == newSelectedItem)
                {
                    //At least select the first camera
                    newSelectedItem = cameraID;
                }
                else if (null != oldSelectedItem)
                {
                    //If the previous selected camera is still available
                    //then prefer this camera.
                    if (string.Compare(newSelectedItem, cameraID, StringComparison.Ordinal) == 0)
                    {
                        newSelectedItem = cameraID;
                    }
                }
            }

            //If available select a camera.
            if (null != newSelectedItem)
            {
                List_Cam.SelectedItem = newSelectedItem;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null == m_Vimba)
            {
                throw new Exception("Vimba has not been started.");
            }
            m_Vimba.Shutdown();
            m_Vimba = null;           
        }
            
        private void bT_OpenCamera_Click(object sender, EventArgs e)
        {
           
            if (!Camera1.IsOpen)
            {
                bool IsOpen= Camera1.OpenCamera();
                if (IsOpen)
               {
                   //-------读取相机参数到界面-----                               
                   Cam1_ExpTime = Camera1.ReadExposureTime();
                   trackBar1.Value = (int)Cam1_ExpTime;

                   Camera1.Set_TriggerSource(1);
                   Cam1_TriggerSource = Camera1.ReadTriggerSource();
                   rBt_Freerun.Checked = true;//触发方式就不反馈到界面了，需要也可以
                   LogMessage("相机打开成功！");
                   Cur_camIsOpen = true;
                   cB_SaveImage.Checked = true;
               }
               else
               {
                   LogMessage("相机打开失败，请检查IP配置");
               }
 
            }
            else
            {
                Camera1.CloseCamera();
                Cur_camIsOpen = false;
                Cur_camIsAcq = false;
            }

            updateControls();            
                
        }
        
        private void bT_Acqure_Click(object sender, EventArgs e)
        {
            if (!(Camera1.IsAcquring))
            {
                Camera1.StartAcquisition(mCam1_OnFrameReceived);//委托图像接收
                Cur_camIsAcq = true;
               
                bT_Acqsingle.Enabled = false;

            }
            else
            {
                Camera1.StopAcquisition();
                Cur_camIsAcq = false;
                
                bT_Acqsingle.Enabled = true;
            }
            updateControls();
        }

        private void mCam1_OnFrameReceived(Image img, ulong timestamp, ulong frameid, VmbFrameStatusType status)
        {
            //// long start = DateTime.Now.Ticks;
            frameCount += 1;
            lemon L1 = new lemon();
            L1.MyImg = img;

            // string filename = DateTime.Now.ToString("MMddHHmmssfff");
            string filename = String.Format("{0:D10}-{1:D20}-{2}", frameid, timestamp, status==VmbFrameStatusType.VmbFrameStatusComplete?"OK":"NG");
            filename = ImgSavePath + "" + filename;

            L1.Timestamp = filename;
            L1.nFrameIndex = frameid;
            L1.status = status;

           
            lock (lockImagequeue)
            {
                if (ImageSavequeue1.Count > 400)//暂存最新的两百张，可以自己根据需要设置
                { ImageSavequeue1.Dequeue(); }

                ImageSavequeue1.Enqueue(L1);
            }
            //tB_Log.AppendText(string.Format("{0:mm:ss.fff}getImage", DateTime.Now) + "\r\n");
            //if (InvokeRequired == true)
            //{
            //    BeginInvoke(new ImageReceivedHandler(this.mCam1_OnFrameReceived), img);
            //    return;
            //}

            if (status == VmbFrameStatusType.VmbFrameStatusComplete)
            {
                m_PictureBox.Image = img;
            }
            else
            {
                String strIncompleteFrameMsg = String.Format("WARNING: bad image, frameid = {0}, status = {1}", frameid, status);
                //LogMessage(strIncompleteFrameMsg);
                System.Diagnostics.Debug.WriteLine(strIncompleteFrameMsg);
                
            }

            //long end  = DateTime.Now.Ticks;
            //double t = (end - start) * 1.0 / TimeSpan.TicksPerSecond * 1000; 
            //在此处可以对图像处理，帧率比较快的话建议在此处另开线程          
        }

        private void cB_SaveImage_CheckedChanged(object sender, EventArgs e)
        {

            if (cB_SaveImage.Checked)
            {
                if (!Directory.Exists(ImgSavePath))
                {
                    LogError("图片存储路径不存在!");
                }
                else
                {
                    SaveModeOn = true;
                    Thread sI = new Thread(Saveimage);
                    sI.Start();
                }
            }
            else 
            {
                SaveModeOn = false;
            }
        }
        private void Saveimage()
        {
           while (SaveModeOn)
            {
                lemon L2 = new lemon();
                int count = 0;
               
                lock (lockImagequeue)
                {
                    count = ImageSavequeue1.Count;
                    if (count > 1)
                    {
                        L2 = ImageSavequeue1.Dequeue();
                    } 
                    else if((!(Camera1.IsAcquring)) && (count > 0))//相机停止采集的时候再去存最后一张，避免冲突
                    {
                        L2 = ImageSavequeue1.Dequeue();
                    }
                }
                
                if (L2.MyImg != null)
                {
                    lock (lockSavemode)
                    {
                        // 4 MB * 45 * 1000 = 180 GB
                        if (saveCount < 45 * 1000)
                        {
                            if (L2.status == VmbFrameStatusType.VmbFrameStatusComplete)
                            {
                                Emgu.CV.Structure.Bgr avgColor = new Emgu.CV.Structure.Bgr(Color.Black);
                                /*
                                Bitmap bmpFrame = L2.MyImg as Bitmap;
                                Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> image = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte>(bmpFrame);

                                //avgColor = image.GetAverage();

                                image.Dispose();
                                image = null;
                                */
                                // TODO: use seperate process to apply OpenCV algorithm for average gray value calculation

                                String strPath = String.Format("{0}-{1:000.0000000}.bmp", L2.Timestamp, avgColor.Green); 
                                L2.MyImg.Save(strPath, ImageFormat.Bmp);
                            }
                            else
                            {
                                // Black image for bad frame from camera
                                L2.MyImg = Image.FromFile("default_error_image.png"); 

                                String strPath = String.Format("{0}-{1:000.0000000}.bmp", L2.Timestamp, 0);
                                L2.MyImg.Save(strPath, ImageFormat.Bmp); 
                            }
                        }

                        saveCount += 1;                     
                    }

                    L2.MyImg.Dispose();
                }
                if (count < 2)
                {
                    Thread.Sleep(1500);
                }

                //GC.Collect();
            }          
        }

        private void updateControls()
        {           
            if (Cur_camIsOpen)
            {
                bT_OpenCamera.Text = "关闭相机";
                p_SetCam1.Enabled = true;//任何关于相机的设置都要在相机打开的前提下，设置触发方式或者曝光时间则尽量要在相机停止采集的状态下
               
                if (Cur_camIsAcq)
                {
                    bT_Acqure.Text = "停止采集/OffLine";
                    sendSoftTrigger = true;
                }
                else
                {
                    bT_Acqure.Text = "开始采集/OnLine";
                    sendSoftTrigger = false;
                }
            }
            else
            {             
                bT_OpenCamera.Text = "打开相机";
                p_SetCam1.Enabled = false;
                sendSoftTrigger = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Cam1_ExpTime = trackBar1.Value;
            Camera1.Set_ExposureTime(Cam1_ExpTime);
            lB_expTime.Text = Cam1_ExpTime.ToString() + " μs";
        }

       

        private void rBt_Freerun_CheckedChanged(object sender, EventArgs e)
        {
            //set Freerun
            if (rBt_Freerun.Checked)
                Camera1.Set_TriggerSource(1);//Freerun
        }
       
        private void rBt_HardTri_CheckedChanged(object sender, EventArgs e)
        {
            //set Line1
            if (rBt_HardTri.Checked )
                Camera1.Set_TriggerSource(2);//Line1
        }
        
        private void rBt_SoftTri_CheckedChanged(object sender, EventArgs e)
        {
            //set Software
            if (rBt_SoftTri.Checked)
            {
                Camera1.Set_TriggerSource(3);
                sendSoftTrigger = true;
                Thread sI = new Thread(SendSoftTri);
                sI.Start();
            }
            else
            {
                sendSoftTrigger = false;
            }

        }
        private void rBt_FixTri_CheckedChanged(object sender, EventArgs e)
        {
            if (rBt_FixTri.Checked)
            {
                Camera1.Set_TriggerSource(4);
                panel_setRate.Visible = true;
                maxRate = Camera1.ReadMaxRate();
                tb_maxRate.Text = maxRate.ToString();
            }
            else
            {
                panel_setRate.Visible = false;
            }
        }
        private void SendSoftTri()
        {
            int SoftTri_interval = (int)Num_softTri.Value;
            while (sendSoftTrigger)
            {
                Thread.Sleep(SoftTri_interval);
                tB_Log.AppendText(string.Format("{0:mm:ss.fff}sendcommand", DateTime.Now) + "\r\n");
                Camera1.SendSoftwareTrigger();
            }
        }
     

        private void panel_Left_Resize(object sender, EventArgs e)//窗口调整
        {
            int pW = panel_Left.Width;
            int pH = panel_Left.Height;
            int pX=panel_Left.Location.X;
            int pY=panel_Left.Location.Y;

           
        }

        private void bT_Acqsingle_Click(object sender, EventArgs e)
        {
            try
            {
                //单张取图方法要在非采集的状态下、并且触发模式为Freerun
                if (!rBt_Freerun.Checked)
                {
                    rBt_Freerun.Checked = true;
                }
                m_PictureBox.Image = Camera1.AcquireSingleImage();
                
            }
            catch { }
        }

        private void List_Cam_SelectedIndexChanged(object sender, EventArgs e)
        {          
            if (Camera1 != null)
            {
                if (Camera1.IsOpen)
                {
                    if (Camera1.IsOpen)
                    {
                        Camera1.StopAcquisition();
                    }
                    Camera1.CloseCamera();
                        
                }
                Camera1 = null;
                Cur_camIsOpen = false;
                Cur_camIsAcq = false;
                
            }
            updateControls();
            if (List_Cam.Items.Count > 0)
            {
                Cur_CameraID = List_Cam.SelectedItem.ToString();
                Init_Camera();
            }
            else
                Cur_CameraID = null;
        }

        private void bT_SelectSavePath_Click(object sender, EventArgs e)
        {
           
            FolderBrowserDialog dialog = new FolderBrowserDialog();           
            dialog.Description = "请选择一个文件夹";           
            dialog.ShowNewFolderButton = true;
            if (defaultPath != "")
            {	          
	            dialog.SelectedPath = defaultPath;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
	            defaultPath = dialog.SelectedPath;
                ImgSavePath = defaultPath +"\\";                
            }
            tb_SavePath.Text = ImgSavePath;
        }

        private void bt_setRate_Click(object sender, EventArgs e)
        {
            if (tB_setRate.Text != "") 
            {
                try
                {
                    double fixedrate = Convert.ToDouble(tB_setRate.Text);
                    if (fixedrate < maxRate)
                    {
                        Camera1.SetRate(fixedrate);
                    }
                    else
                    {
                        LogMessage("设定值不能超过允许的最大值");
                    }
                }
                catch {
                    LogError("参数异常！");
                }               
            } 
        }

        private void button_images_analyze_Click(object sender, EventArgs e)
        { 
            Thread th = new Thread(ThreadChild);
            Console.WriteLine("Main Thread Start!"); 

            th.Start();

        }


        private void ThreadChild()
        {
            Console.WriteLine("Child Thread Start!");

            StreamWriter F = new StreamWriter(ImgSavePath + "\\result.csv", false);
            //F.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, ", "Missed Frame", idx, 0, "NG", 0, 0);

            //C#遍历指定文件夹中的所有文件
            DirectoryInfo TheFolder = new DirectoryInfo(ImgSavePath);

            /*            //遍历文件夹
                        foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                            this.listBox1.Items.Add(NextFolder.Name);
            */
            ulong frameidPrev = 0;
            //遍历文件
            FileInfo[] files = TheFolder.GetFiles("*.bmp");
            String[] filesName = new String[files.Length];
            int i = 0;
            foreach (FileInfo NextFile in files)
            {
                filesName[i] = NextFile.Name;
                i++; 
            }
            Array.Sort(filesName);

            Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> image = null;
            foreach (String nextFile in filesName)
            {
                String[] strParts = ParseFileName(nextFile);
                ulong frameid = ulong.Parse(strParts[0]);
                ulong timestamp = ulong.Parse(strParts[1]);
                String strStatus = strParts[2];
                double average = double.Parse(strParts[3]);

                if (frameidPrev == 0)
                    frameidPrev = frameid;

                // 非连续的正常帧序列
                if (frameid > 0 && frameid - frameidPrev >= 2)
                {
                    ulong diff = frameid - frameidPrev;
                    for (ulong idx = frameidPrev + 1; idx < frameid; idx++)
                    {
                        Console.WriteLine("file: {0}, {1}, {2}, {3}, {4}, avg: {5}", "Missed Frame", idx, 0, "NG", 0, 0);
                        F.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, ", "Missed Frame", idx, 0, "NG", 0, 0);

                    }
                }


                // 
                {
                    image = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte>(ImgSavePath + "\\" + nextFile);
                    Emgu.CV.Structure.Bgr avgColor = image.GetAverage();
                    image.Dispose();

                    Console.WriteLine("file: {0}, {1}, {2}, {3}, {4}, avg: {5}", nextFile, frameid, timestamp, strStatus, average, avgColor.Green);
                    F.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, ", nextFile, frameid, timestamp, strStatus, average, avgColor.Green); 
                }


                frameidPrev = frameid;
            }

            image = null;

            F.Close();
            Console.WriteLine("Child Thread Ended!");
        }

        private String[] ParseFileName(String name)
        {
            String[] strParts = { "0", "0", "NG", "0" };
            String filename = name.Replace(".bmp", "");
            return filename.Split('-'); 
        }


    }

}
