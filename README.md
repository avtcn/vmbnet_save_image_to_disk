# vimba_csharp_save_image_to_disk
Save freerun images into ssd disk for verification or other algorithms processing.  

plot "truking-log.csv" every ::2 u 2:6 w l title "FirstFrame" lc "blue", "truking-log.csv" every ::2 u 2:8 w l title "16Frames" lc "#FF0000",  "truking-log.csv" every ::2 u 2:10 w l title "ImageProceTime" lc "gray"



# Show data in Plot from result.csv


## Average Gray vs TimeStamp vs Center Pixel

```

set yrange [100:250]
set ytics nomirror 
set autoscale xy
plot "result.csv" every ::2 u 2:6 w l title "Average Gray" lc "blue" axis x1y1, "result.csv" every ::2 u 2:3 w l title "TimeStamp" lc "red" axis x1y2, "result.csv" every ::2 u 2:6 w l title "Average Gray" lc "green" axis x1y1, 
set grid 
set autoscale
replot 

plot "result.csv" every ::2 u 2:6 w l title "Average Gray" lc "blue" axis x1y1, "result.csv" every ::2 u 2:3 w l title "TimeStamp" lc "red" axis x1y2, "result.csv" every ::2 u 2:7 w l title "Center Point Pixel" lc "green" axis x1y1,   


```


## Center pixel value of image

```

plot "result.csv" every ::2 u 2:6 w l title "Average Gray" lc "blue" axis x1y1, 


plot "result.csv" every ::2 u 2:7 w l title "Center Point Pixel" lc "green" axis x1y1, 


plot "result.csv" every ::2 u 2:6 w l title "Average Gray" lc "blue" axis x1y1, "result.csv" every ::2 u 2:7 w l title "Center Point Pixel" lc "green" axis x1y1, 


```
