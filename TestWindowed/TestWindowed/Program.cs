using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.IO;

namespace TestWindowed
{
    class Program
    {

        static Window w;
        System.Windows.Controls.Image i;
        static WriteableBitmap writeableBitmap;
        static MenuItem loadImage;

        static MenuItem rotImage;

        static Program instance;

        static int angle = 45;

        int width = 1400;

        int height = 800;

        public Program()
        {

            instance = this;

            i = new System.Windows.Controls.Image();

            RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(i, EdgeMode.Aliased);

            w = new Window();

            w.Content = i;

            w.Show();

            System.Windows.Controls.ContextMenu programMenu = new ContextMenu();

            loadImage = new MenuItem();

            loadImage.Header = "Load Image";

            loadImage.Click += new RoutedEventHandler(this.loadItem);

            rotImage = new MenuItem();

            rotImage.Header = "Rotate Image";

            rotImage.Click += new RoutedEventHandler(this.rotateImage);

            programMenu.Items.Add(loadImage);

            programMenu.Items.Add(rotImage);

            w.ContextMenu = programMenu;

            w.Width = width;

            w.Height = height;

            writeableBitmap = new WriteableBitmap(
                (int)w.ActualWidth,
                (int)w.ActualHeight,
                96,
                96,
                PixelFormats.Rgb24,
                null);

            i.Source = writeableBitmap;

            i.Stretch = Stretch.None;
            i.HorizontalAlignment = HorizontalAlignment.Left;
            i.VerticalAlignment = VerticalAlignment.Top;

            i.MouseMove += new MouseEventHandler(i_MouseMove);
            i.MouseLeftButtonDown +=
                new MouseButtonEventHandler(i_MouseLeftButtonDown);
            i.MouseRightButtonDown +=
                new MouseButtonEventHandler(i_MouseRightButtonDown);

            w.KeyDown += new KeyEventHandler(i_KeyDown);

            w.KeyUp += new KeyEventHandler(i_KeyUp);

            w.MouseWheel += new MouseWheelEventHandler(w_MouseWheel);

            w.ContentRendered += W_ContentRendered;

            Application app = new Application();
            app.Run();

        }

        [STAThread]
        public static void Main(string[] args)
        {

            Program program = new Program();
                        
        }

        private void loadItem(object sender, EventArgs e)
        {

            Debug.WriteLine("Load Item Context Menu Clicked!");

            Debug.WriteLine("Starting Finished Rotating!");

            //System.Drawing.Image loadedImage = System.Drawing.Image.FromFile("C:/Users/rdegr/Pictures/Camera Roll/WIN_20190616_15_33_06_Pro.jpg");

            Bitmap testBitmap = (Bitmap)(Bitmap.FromFile("D:/vs_code_projects/c#_project/TestWindowed/TestWindowed/test_symmetric_3.bmp"));

            /*

            FileStream inputFileStream = new FileStream("D:/vs_code_projects/c#_project/TestWindowed/TestWindowed/test_symmetric_2.bmp", FileMode.Open);

            BitmapDecoder bitmapDecoder = new BmpBitmapDecoder(inputFileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            BitmapSource bitmapSource = bitmapDecoder.Frames[0];

            //bitmapSource.

            int imageWidth = (int)bitmapSource.Width;

            int imageHeight = (int)bitmapSource.Height;

            //Bitmap convertedWriteable = (Bitmap)writeableBitmap;

            int bitsPerPixel = bitmapSource.Format.BitsPerPixel;

            //int numberOfActiveBytes = imageWidth * imageHeight;

            int numberOfActiveBytes = imageWidth * imageHeight * (bitsPerPixel / 8);

            //int numberOfActiveBytes = imageWidth * imageHeight * bitsPerPixel;

            Int32[] imageBits = new Int32[numberOfActiveBytes];

            int rawStride = (width * bitsPerPixel + 7) / 8;

            Debug.WriteLine("Image Bytes Count: " + imageBits.Count() + " Bits Per Pixel: " + bitsPerPixel + " bytes Per Pixel: " + (bitsPerPixel / 8));

            //Array pixels = Array.CreateInstance(typeof(bool),numberOfActiveBytes);

            bitmapSource.CopyPixels(imageBits, rawStride, 0);

            //byte[] imageBytes = pixels.

            // Graphics canvasGraphics = Graphics.FromImage(w);

            // canvasGraphics.DrawImage(loadedImage, new System.Drawing.Point(25, 25));

            //byte[] imageBytes = ConvertBoolArrayToByteArray(imageBits);

            */

            TranslateBitmap(testBitmap);

            Debug.WriteLine("Finished Drawing Image!");

            Int32[] testArray = new int[3];

            Int32 test1 = 1;
            Int32 test2 = 2;
            Int32 test3 = 4;

            testArray[0] = test1;
            testArray[1] = test2;
            testArray[2] = test3;

            //PrintIntArrayBinary(testArray);

            Debug.WriteLine("Printing Derived Array");

            //PrintIntArrayBinary(imageBits);

            // i.Source = bitmapSource;

            //testBitmap.GetPixel(0, 0);
            
            //BitmapSource testBitmapSource = bitmapDecoder.Frames[0];

            //i.Source = testBitmapSource;

        }

        static void TranslateBitmap(Bitmap bitmap)
        {

            try
            {
                // Reserve the back buffer for updates.
                writeableBitmap.Lock();
                for (int xCounter = 0; xCounter < bitmap.Width; xCounter++)
                {
                    for(int yCounter = 0; yCounter < bitmap.Height; yCounter++)
                    {

                        unsafe
                        {

                            // Get a pointer to the back buffer.
                            int pBackBuffer = (int)writeableBitmap.BackBuffer;

                            // Find the address of the pixel to draw.
                            pBackBuffer += yCounter * writeableBitmap.BackBufferStride;
                            pBackBuffer += xCounter * 3;

                            System.Drawing.Color pixelColor = bitmap.GetPixel(xCounter, yCounter);

                            // Compute the pixel's color.
                            int color_data = ((int)pixelColor.R << 0); //R
                            color_data |= ((int)pixelColor.G << 8);   // G
                            color_data |= ((int)pixelColor.B << 16);   // B

                            // Assign the color data to the pixel.
                            *((int*)pBackBuffer) = color_data;
                        }
                        // Debug.WriteLine("X and Y values are: " + ((dataCounter / 3) % width) + " : " + ((dataCounter / 3) / width));

                        // Specify the area of the bitmap that changed.
                        writeableBitmap.AddDirtyRect(new Int32Rect(xCounter, yCounter, 1, 1));

                    }
                    
                }
            }
            finally
            {
                // Release the back buffer and make it available for display.
                writeableBitmap.Unlock();
            }

        }

        private void PrintIntArrayBinary(Int32[] intArray)
        {

            for (int intCounter = 0; intCounter < intArray.Count(); intCounter++)
            {

                byte b = 0;

                Debug.WriteLine("Int Number: " + intCounter);

                for (int bitCounter = 32; bitCounter >= 0; bitCounter--)
                {

                    byte one = 1;

                    one <<= bitCounter;

                    if ((intArray[intCounter] & one) > 0)
                    {

                        Debug.Write("1");

                    }
                    else
                    {

                        Debug.Write("0");

                    }

                }

                Debug.WriteLine("");
            }

        }

        private byte[] ConvertBoolArrayToByteArray(bool[] boolArray)
        {

            byte[] byteArray = new byte[boolArray.Count() / 8];

            for(int byteCounter = 0; byteCounter < byteArray.Count(); byteCounter++)
            {

                byte b = 0;

                for(int boolCounter = byteCounter * 8, shiftCounter = 7; boolCounter < (byteCounter * 8) + 8; boolCounter++, shiftCounter--)
                {

                    if (boolArray[boolCounter])
                    {

                        byte one = 1;

                        one <<= shiftCounter;

                        b |= one;

                    }

                }

                byteArray[byteCounter] = b;

            }

            return byteArray;

        }

        /*
        private void loadItem(object sender, EventArgs e)
        {

            Debug.WriteLine("Load Item Context Menu Clicked!");

            Debug.WriteLine("Starting Finished Rotating!");

            System.Drawing.Image loadedImage = System.Drawing.Image.FromFile("C:/Users/rdegr/Pictures/Camera Roll/WIN_20190616_15_33_06_Pro.jpg");

            byte[] imageBytes = System.IO.File.ReadAllBytes("D:/vs_code_projects/c#_project/TestWindowed/TestWindowed/test_symmetric_2.bmp");

            byte[] rotateImageBytes = new byte[imageBytes.Count()];


            for (int zeroCounter = 0, maxCounter = rotateImageBytes.Count() - 1; zeroCounter < imageBytes.Count(); zeroCounter++, maxCounter--)
            {

                rotateImageBytes[maxCounter] = imageBytes[zeroCounter];

            }

            PixelFormat pf = PixelFormats.Rgb24;

            int width = 480;

            int height = 480;

            int rawStride = (width * pf.BitsPerPixel) / 8;

            //int rawStride = width / 8;

            // int rawStride = (width * pf.BitsPerPixel + 7) / 8;

            // byte[] rawImage = new byte[rawStride * height];

            byte[] rawImage = rotateImageBytes;

            // Initialize the image with data.
            //Random value = new Random();
            //value.NextBytes(rawImage);

            // Create a BitmapSource.

            // begin block comment !@#$

            //BitmapSource bitmap = BitmapSource.Create(width, height,
            //    96, 96, pf, null,
            //    rawImage, rawStride);

            // end block comment !@#$
            

            // Create an image element;
            //Image myImage = new Image();
            //myImage.Width = 200;
            // Set image source.
            //myImage.Source = bitmap;

            //instance.i = new Image();

            //instance.i.Loaded += new RoutedEventHandler(rotateImage);

            // begin block comment !@#$

            ((Image)instance.i).Source = bitmap;

            // end block comment !@#$

            DrawImagePixels(rawImage, width, height);

            //Thread rotateThread = new Thread(rotateImage);

            //rotateThread.Start();

            //RotateTransform rotate = new RotateTransform();

            //rotate.Angle = 180;

            //((Image)instance.i).LayoutTransform = rotate;

            Debug.WriteLine("Finished Rotating Thread1!");

        }
        */

        private void rotateImage(object sender, EventArgs e)
        {

            Debug.WriteLine("Started Rotating Thread2!");

            RotateTransform rotate = new RotateTransform();

            rotate.Angle = angle;

            angle += 45;

            System.Windows.Media.Matrix m = instance.i.RenderTransform.Value;

            //m.

                m.ScaleAt(
                    1.0 / 1.5,
                    1.0 / 1.5,
                    50,
                    50);

                (instance.i).LayoutTransform = rotate;

            Debug.WriteLine("Finished Rotating Thread2!");

        }

        private static void W_ContentRendered(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            ClearCanvas();

        }

        static void ClearCanvas()
        {
            //int column = (int)e.GetPosition(i).X;
            //int row = (int)e.GetPosition(i).Y;

            for(int column = 0; column < writeableBitmap.Width; column++)
            {

                for(int row = 0; row < writeableBitmap.Height; row++)
                {

                    try
                    {
                        // Reserve the back buffer for updates.
                        writeableBitmap.Lock();

                        unsafe
                        {
                            // Get a pointer to the back buffer.
                            int pBackBuffer = (int)writeableBitmap.BackBuffer;

                            // Find the address of the pixel to draw.
                            pBackBuffer += row * writeableBitmap.BackBufferStride;
                            pBackBuffer += column * 3;

                            // Compute the pixel's color.
                            int color_data = 255 << 16; // R
                            color_data |= 255 << 8;   // G
                            color_data |= 255 << 0;   // B

                            // Assign the color data to the pixel.
                            *((int*)pBackBuffer) = color_data;
                        }

                        // Specify the area of the bitmap that changed.
                        writeableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
                    }
                    finally
                    {
                        // Release the back buffer and make it available for display.
                        writeableBitmap.Unlock();
                    }

                }

            }

        }

        static void DrawImagePixels(Int32[] data, int totalDataSize, int width, int height)
        {

            try
            {
                // Reserve the back buffer for updates.
                writeableBitmap.Lock();
                for (int dataCounter = 0; dataCounter < data.Count(); dataCounter++)
                {

                    unsafe
                    {

                        // Get a pointer to the back buffer.
                        int pBackBuffer = (int)writeableBitmap.BackBuffer;

                        // Find the address of the pixel to draw.
                        pBackBuffer += ((dataCounter / 3) / width) * writeableBitmap.BackBufferStride;
                        pBackBuffer += ((dataCounter / 3) % width) * 3;

                        // Compute the pixel's color.
                        int color_data = ((int)data[dataCounter] << 16); // R
                        color_data |= ((int)data[dataCounter] << 8);   // G
                        color_data |= ((int)data[dataCounter] << 0);   // B

                        // Assign the color data to the pixel.
                        *((int*)pBackBuffer) = color_data;
                    }
                    // Debug.WriteLine("X and Y values are: " + ((dataCounter / 3) % width) + " : " + ((dataCounter / 3) / width));

                    // Specify the area of the bitmap that changed.
                    writeableBitmap.AddDirtyRect(new Int32Rect(((dataCounter / 3) % width), ((dataCounter / 3) / width), 1, 1));
                }
            }
            finally
            {
                // Release the back buffer and make it available for display.
                writeableBitmap.Unlock();
            }

            

            
        }

        /*
        static void DrawImagePixels(byte[] data, int totalBytes, int width, int height)
        {

            try
            {
                // Reserve the back buffer for updates.
                writeableBitmap.Lock();
                for (int dataCounter = 0; dataCounter < totalBytes; dataCounter += 3)
                {

                    unsafe
                    {

                        // Get a pointer to the back buffer.
                        int pBackBuffer = (int)writeableBitmap.BackBuffer;

                        // Find the address of the pixel to draw.
                        pBackBuffer += ((dataCounter / 3) / width) * writeableBitmap.BackBufferStride;
                        pBackBuffer += ((dataCounter / 3) % width) * 3;

                        // Compute the pixel's color.
                        int color_data = ((int)data[dataCounter] << 16); // R
                        color_data |= ((int)data[dataCounter + 1] << 8);   // G
                        color_data |= ((int)data[dataCounter + 2] << 0);   // B

                        // Assign the color data to the pixel.
                        *((int*)pBackBuffer) = color_data;
                    }
                    // Debug.WriteLine("X and Y values are: " + ((dataCounter / 3) % width) + " : " + ((dataCounter / 3) / width));

                    // Specify the area of the bitmap that changed.
                    writeableBitmap.AddDirtyRect(new Int32Rect(((dataCounter / 3) % width), ((dataCounter / 3) / width), 1, 1));
                }
            }
            finally
            {
                // Release the back buffer and make it available for display.
                writeableBitmap.Unlock();
            }

        }
        */

        static void DrawPixel(MouseEventArgs e)
        {
            int column = (int)e.GetPosition(instance.i).X;
            int row = (int)e.GetPosition(instance.i).Y;

            try
            {
                // Reserve the back buffer for updates.
                writeableBitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    int pBackBuffer = (int)writeableBitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += row * writeableBitmap.BackBufferStride;
                    pBackBuffer += column * 3;

                    // Compute the pixel's color.
                    int color_data = 0 << 16; // R
                    color_data |= 0 << 8;   // G
                    color_data |= 0 << 0;   // B

                    // Assign the color data to the pixel.
                    *((int*)pBackBuffer) = color_data;
                }

                // Specify the area of the bitmap that changed.
                writeableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                writeableBitmap.Unlock();
            }
        }

        static void ErasePixel(MouseEventArgs e)
        {
            byte[] ColorData = { 0, 0, 0, 0 }; // B G R

            Int32Rect rect = new Int32Rect(
                    (int)(e.GetPosition(instance.i).X),
                    (int)(e.GetPosition(instance.i).Y),
                    1,
                    1);

            writeableBitmap.WritePixels(rect, ColorData, 4, 0);
        }

        static void i_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ErasePixel(e);
        }

        static void i_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (!wDown)
            {

                DrawPixel(e);

            }
            else
            {

                PanView(e, null);

            }

        }

        static void PanView(MouseButtonEventArgs buttonE, MouseEventArgs e)
        {

            Debug.WriteLine("Panning");

            System.Windows.Media.Matrix m = instance.i.RenderTransform.Value;

            //m.

            
            if (buttonE == null)
            {

                Debug.WriteLine("Relative to Window x,y: " + e.GetPosition(w).X + " : " + e.GetPosition(w).Y);

                Debug.WriteLine("Relative to Instance x,y: " + e.GetPosition(instance.i).X + " : " + e.GetPosition(instance.i).Y);

                Debug.WriteLine("Relative to Window x,y adjusted: " + (e.GetPosition(w).X - (w.Width / 2)) + " : " + (e.GetPosition(w).Y - (w.Height / 2)));

                m.Translate(
                
                -(e.GetPosition(w).X - (instance.width / 2)),
                -(e.GetPosition(w).Y - (instance.height / 2)));
                

                /*
                m.Transform(new Point(e.GetPosition(instance.i).X,
                e.GetPosition(instance.i).Y));
                */

    }
            else
            {

                Debug.WriteLine("Relative to Window x,y: " + buttonE.GetPosition(w).X + " : " + buttonE.GetPosition(w).Y);

                Debug.WriteLine("Relative to Instance x,y: " + buttonE.GetPosition(instance.i).X + " : " + buttonE.GetPosition(instance.i).Y);

                Debug.WriteLine("Relative to Window x,y adjusted: " + (buttonE.GetPosition(w).X - (w.Width / 2)) + " : " + (buttonE.GetPosition(w).Y - (w.Height / 2)));
                
                m.Translate(

                -(buttonE.GetPosition(w).X - (instance.width / 2)),
                -(buttonE.GetPosition(w).Y - (instance.height / 2)));

                //buttonE.GetPosition(w).X,
                //buttonE.GetPosition(w).Y);


                /*
                m.Transform(new Point(buttonE.GetPosition(instance.i).X,
                buttonE.GetPosition(instance.i).Y));
                */

            }

            instance.i.RenderTransform = new MatrixTransform(m);

            Debug.WriteLine("Finished Panning");

        }

        static Boolean wDown = false;

        static void i_KeyDown(object sender, KeyEventArgs e)
        {

            //Debug.WriteLine("Key Down: " + e.Key);

            if (e.Key == Key.W)
            {

                //Debug.WriteLine("W Key is Down!");

                wDown = true;

            }

        }

        static void i_KeyUp(object sender, KeyEventArgs e)
        {

            //Debug.WriteLine("Key Up: " + e.Key);

            if (e.Key == Key.W)
            {

                //Debug.WriteLine("W Key is Up!");

                wDown = false;

            }

        }

        static void i_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!wDown)
                {

                    DrawPixel(e);

                }
                else
                {

                    PanView(null, e);

                }
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                ErasePixel(e);
            }
        }

        static void w_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            System.Windows.Media.Matrix m = instance.i.RenderTransform.Value;

            //m.

            if (e.Delta > 0)
            {
                m.ScaleAt(
                    1.5,
                    1.5,
                    e.GetPosition(w).X,
                    e.GetPosition(w).Y);
            }
            else
            {
                m.ScaleAt(
                    1.0 / 1.5,
                    1.0 / 1.5,
                    e.GetPosition(w).X,
                    e.GetPosition(w).Y);
            }

            instance.i.RenderTransform = new MatrixTransform(m);
        }

    }
}
