using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using AVT.VmbAPINET;
namespace CameraViewer1
{
    public delegate void ImageReceivedHandler(Image img, ulong time, ulong frameid, VmbFrameStatusType status);//changed by Lemon 0521
    class AVT_Cam
    {      
        
        private ImageReceivedHandler ImageHandler = null;
        public bool IsOpen {get;set;}
        public bool IsAcquring { get; set; }
        public Camera m_Cam {get; set;}

        public AVT_Cam() 
        {
            IsOpen = false;
            IsAcquring = false;
            m_Cam = null;

        }

        private Image ConvertFrame1(Frame frame)
        {
            if (null == frame)
            {
                throw new ArgumentNullException("frame");
            }

            //Check if the image is valid
            if (VmbFrameStatusType.VmbFrameStatusComplete != frame.ReceiveStatus)
            {
                throw new Exception("Invalid frame received. Reason: " + frame.ReceiveStatus.ToString());
            }
            // m_Cam.QueueFrame(frame);

            //Convert raw frame data into image (for image display)
            Image image = null;
            switch (frame.PixelFormat)
            {
                case VmbPixelFormatType.VmbPixelFormatMono8:
                case VmbPixelFormatType.VmbPixelFormatMono10:
                case VmbPixelFormatType.VmbPixelFormatMono12:
                case VmbPixelFormatType.VmbPixelFormatMono14:
                    {
                       // Bitmap bitmap = new Bitmap((int)frame.Width, (int)frame.Height, PixelFormat.Format8bppIndexed);该方法会得到异常的图像
                       
                       Bitmap bitmap = null;
                       frame.Fill(ref bitmap);                      
                       image = bitmap;
                    }
                    break;

                case VmbPixelFormatType.VmbPixelFormatBgr8:
                case VmbPixelFormatType.VmbPixelFormatRgb8:
                case VmbPixelFormatType.VmbPixelFormatBayerRG8:
                case VmbPixelFormatType.VmbPixelFormatBayerGR8:
                    {
                        Bitmap bitmap = new Bitmap((int)frame.Width, (int)frame.Height, PixelFormat.Format24bppRgb);
                        frame.Fill(ref bitmap);
                        image = bitmap;
                    }
                    break;

                default:
                    throw new Exception("Current pixel format is not supported by this example (only Mono8 and BRG8Packed are supported).");
            }

            return image;
        }

        private Image ConvertFrame(Frame frame)
        {
            if (null == frame)
            {
                throw new ArgumentNullException("frame");
            }
            
            //Check if the image is valid
            if (VmbFrameStatusType.VmbFrameStatusComplete != frame.ReceiveStatus)
            {
                throw new Exception("Invalid frame received. Reason: " + frame.ReceiveStatus.ToString());
            }
            // m_Cam.QueueFrame(frame);

            //Convert raw frame data into image (for image display)
            Image image = null;
            switch (frame.PixelFormat)
            {
                case VmbPixelFormatType.VmbPixelFormatMono8:
                    {
                        Bitmap bitmap = new Bitmap((int)frame.Width, (int)frame.Height, PixelFormat.Format8bppIndexed);

                        //Set greyscale palette
                        ColorPalette palette = bitmap.Palette;
                        for (int i = 0; i < palette.Entries.Length; i++)
                        {
                            palette.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        bitmap.Palette = palette;

                        //Copy image data
                        BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, (int)frame.Width, (int)frame.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        try
                        {
                            //Copy image data line by line
                            for (int y = 0; y < (int)frame.Height; y++)
                            {
                                System.Runtime.InteropServices.Marshal.Copy(frame.Buffer, y * (int)frame.Width, new IntPtr(bitmapData.Scan0.ToInt64() + y * bitmapData.Stride), (int)frame.Width);
                            }
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }

                        image = bitmap;
                    }
                    break;

                case VmbPixelFormatType.VmbPixelFormatBgr8:
                    {
                        Bitmap bitmap = new Bitmap((int)frame.Width, (int)frame.Height, PixelFormat.Format24bppRgb);

                        //Copy image data
                        BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, (int)frame.Width, (int)frame.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                        try
                        {
                            //Copy image data line by line
                            for (int y = 0; y < (int)frame.Height; y++)
                            {
                                System.Runtime.InteropServices.Marshal.Copy(frame.Buffer,
                                                                                y * ((int)frame.Width) * 3,
                                                                                new IntPtr(bitmapData.Scan0.ToInt64() + y * bitmapData.Stride),
                                                                                ((int)(frame.Width) * 3));
                            }
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }

                        image = bitmap;
                    }
                    break;

                default:
                    throw new Exception("Current pixel format is not supported by this example (only Mono8 and BRG8Packed are supported).");
            }

            return image;
        }
        private void AdjustPixelFormat(Camera camera)
        {
            if (null == camera)
            {
                throw new ArgumentNullException("camera");
            }

            string[] supportedPixelFormats = new string[] { "BGR8Packed", "Mono8", "BayerRG8", "BayerGR8", "RGB8Packed" };//使用ConvertFrame1方法支持更多格式
            //Check for compatible pixel format
            Feature pixelFormatFeature = camera.Features["PixelFormat"];

            //Determine current pixel format
            string currentPixelFormat = pixelFormatFeature.EnumValue;

            //Check if current pixel format is supported
            bool currentPixelFormatSupported = false;
            foreach (string supportedPixelFormat in supportedPixelFormats)
            {
                if (string.Compare(currentPixelFormat, supportedPixelFormat, StringComparison.Ordinal) == 0)
                {
                    currentPixelFormatSupported = true;
                    break;
                }
            }

            //Only adjust pixel format if we not already have a compatible one.
            if (false == currentPixelFormatSupported)
            {
                //Determine available pixel formats
                string[] availablePixelFormats = pixelFormatFeature.EnumValues;

                //Check if there is a supported pixel format
                bool pixelFormatSet = false;
                foreach (string supportedPixelFormat in supportedPixelFormats)
                {
                    foreach (string availablePixelFormat in availablePixelFormats)
                    {
                        if ((string.Compare(supportedPixelFormat, availablePixelFormat, StringComparison.Ordinal) == 0)
                            && (pixelFormatFeature.IsEnumValueAvailable(supportedPixelFormat) == true))
                        {
                            //Set the found pixel format
                            pixelFormatFeature.EnumValue = supportedPixelFormat;
                            pixelFormatSet = true;
                            break;
                        }
                    }

                    if (true == pixelFormatSet)
                    {
                        break;
                    }
                }

                if (false == pixelFormatSet)
                {
                    throw new Exception("None of the pixel formats that are supported by this example (Mono8 and BRG8Packed) can be set in the camera.");
                }
            }
        }

       
        public void Set_TriggerSource(int Tri_index)
        {
            if (IsOpen)
            {
                switch (Tri_index)
                {
                    case 1:
                        this.m_Cam.Features["TriggerSource"].EnumValue = "Freerun";
                        break;
                    case 2:
                        this.m_Cam.Features["TriggerSource"].EnumValue = "Line1";
                        break;
                    case 3:
                        this.m_Cam.Features["TriggerSource"].EnumValue = "Software";
                        break;
                    case 4:
                        this.m_Cam.Features["TriggerSource"].EnumValue = "FixedRate";
                        break;
                }
                // And switch it on or off
                this.m_Cam.Features["TriggerMode"].EnumValue = "On";
            }
        }
        
        public string ReadTriggerSource()
        {
            if (IsOpen)
            {
                return m_Cam.Features["TriggerSource"].EnumValue;
            }
            else
            {
                return "Freerun";
            }
        }
        public double ReadMaxRate()
        {
            if (IsOpen)
            {
                return m_Cam.Features["AcquisitionFrameRateLimit"].FloatValue;
            }
            else
            {
                return 1;
            }
        }
        public void SetRate(double Ratevalue)
        {
            if (IsOpen)
            {
                m_Cam.Features["AcquisitionFrameRateAbs"].FloatValue = Ratevalue;
            }
        }
        public double ReadExposureTime()
        {
            if (IsOpen)
            {
                return m_Cam.Features["ExposureTimeAbs"].FloatValue;
            }
            else
            {
                return 100;
            }
        }
        public void Set_ExposureTime(double time)
        {
            if (IsOpen)
            {
                m_Cam.Features["ExposureTimeAbs"].FloatValue = time;
            }

        }
        public bool OpenCamera()
        {
            try
            {
                this.m_Cam.Open(VmbAccessModeType.VmbAccessModeFull);  
           
                m_Cam.OnFrameReceived += new Camera.OnFrameReceivedHandler(m_CamOnFrameReceived);//注册图像接收事件

                if (m_Cam.InterfaceType == VmbInterfaceType.VmbInterfaceEthernet)
                {
                    m_Cam.Features["GVSPAdjustPacketSize"].RunCommand();//自动调整数据包大小
                    while (false == m_Cam.Features["GVSPAdjustPacketSize"].IsCommandDone())
                    {
                        // Do Nothing
                    }
                }

               // AdjustPixelFormat(m_Cam);

                IsOpen = true;
                IsAcquring = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CloseCamera()
        {
            try
            {
                m_Cam.OnFrameReceived -= m_CamOnFrameReceived;
                if (IsAcquring)//实际上直接关闭相机也可以
                {
                    m_Cam.StopContinuousImageAcquisition();
                }
                m_Cam.Close();
                IsOpen = false;
                IsAcquring = false;
            }
            catch
            {

            }
        }

        public void SendSoftwareTrigger()
        {
            if (null != this.m_Cam)
            {
                
                this.m_Cam.Features["TriggerSoftware"].RunCommand();
               
                while (false == m_Cam.Features["TriggerSoftware"].IsCommandDone())
                {
                    // Do nothing
                }
               // start = DateTime.Now.Ticks;
            }
        }
       
        private void m_CamOnFrameReceived(Frame frame)
        {

            //long start = DateTime.Now.Ticks;
            if (VmbFrameStatusType.VmbFrameStatusComplete == frame.ReceiveStatus)
            {
                Image img = null;

                ulong timestamp = frame.Timestamp;//added by Lemon 0521 
                //img = ConvertFrame(frame);
                img = ConvertFrame1(frame);//测试直接将原图转换成bmp
                                           // long end = DateTime.Now.Ticks;

                ImageHandler(img, timestamp, frame.FrameID, frame.ReceiveStatus);  //changed by Lemon 0521              
            }
            else { 
                // for bad frame, also record it
                ulong timestamp = frame.Timestamp;//added by Lemon 0521 
                // For incomplete frame, only default image will be saved
                Image img = null; 
                ImageHandler(img, timestamp, frame.FrameID, frame.ReceiveStatus);  //changed by Lemon 0521              
            }

            try
            {
                m_Cam.QueueFrame(frame);
            }
            catch
            {
                // Do nothing
                //System.Diagnostics.Debug.WriteLine("camera " + index + ":  name is " + m_Camera.Name + ", model is " + m_Camera.Model);
            }
            //long end = DateTime.Now.Ticks;
            //double timeacq = (end - start) * 1.0 / TimeSpan.TicksPerSecond * 1000;
        }

        public void StartAcquisition(ImageReceivedHandler ImageReceived)
        {
            //CameraIndex = index;
            ImageHandler = ImageReceived;
            if (!IsOpen)
            {
                throw new NullReferenceException("No camera retrieved.");
            }          
            try
            {              
                m_Cam.StartContinuousImageAcquisition(3);
                //m_Cam.Features["AcquisitionStart"].RunCommand();//作用和上一句相同，但是标准写法是这一句
                IsAcquring = true;
            }
            catch { }
  
        }

        public void StopAcquisition()
        {
            if (!IsOpen)
            {
                throw new NullReferenceException("No camera retrieved.");
            }
            try
            {
                m_Cam.StopContinuousImageAcquisition();
                IsAcquring = false;
                //m_Cam.Features["AcquisitionStop"].RunCommand();//作用和上一句相同，但是标准写法是这一句
            }
            catch { }
        
        }
      
        public Image AcquireSingleImage()
        {

            // Open camera          
            if (!IsOpen)
            {
                throw new NullReferenceException("No camera retrieved.");
            }
            
            Frame frame = null;
            try
            {
                //Set a compatible pixel format
                AdjustPixelFormat(this.m_Cam);
                this.m_Cam.AcquireSingleImage(ref frame, 2000);
                return ConvertFrame(frame);
            }
            catch
            {
                return null;
            }
           
        }
    }
}
