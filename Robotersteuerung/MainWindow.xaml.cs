using System.Linq;
using System.Windows.Input;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using Coding4Fun.Kinect.Wpf;




    public class Vector
    {
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
    public partial class MainWindow : Window
    {
        #region Variable
        public bool Continue;
        public System.IO.Ports.SerialPort _serialport = new System.IO.Ports.SerialPort();
        public int ScreenMaxY = 100;
        public int ScreenMaxX = 100;

        private float closestDistance = 5f;
        private int closestID = 0;

        public bool continuee
        {
            get { return Continue; } set { Continue = value; }
        }
        public System.IO.Ports.SerialPort serialport
        {
            get { return _serialport; }
            set { _serialport = value; }
        }
        #endregion

        #region Arduino Funktionen

        //Koordinaten werden an dem Arduino Board gesendet
        private void ArduinoSendByteNEw(Single lelle, Single lschulterxy, Single lschulterz, Single kopf, Single rschulterz, Single rschultery, Single relle,Single hals)
        {
            var a = 0;
            var b = 0;
            var c = 0;
            var d = 0;
            var e = 0;
            var f = 0;
            var g = 0;
            var h = 0;
            if (!Double.IsNaN(lelle) && !Double.IsNaN(lschulterxy) && !Double.IsNaN(lschulterz) && !Double.IsNaN(kopf) && !Double.IsNaN(rschultery) && !Double.IsNaN(rschulterz) && !Double.IsNaN(relle))
            {
                if (lschulterz < 0)
                { lschulterz = lschulterz * (-1); }
                if (rschulterz < 0)
                { rschulterz = rschulterz * (-1); }

                a = Math.Abs(Convert.ToInt32(lelle));
                b = Math.Abs(Convert.ToInt32(lschulterxy));
                c = Math.Abs(Convert.ToInt32(lschulterz));
                d = Math.Abs(Convert.ToInt32(kopf));
                e = Math.Abs(Convert.ToInt32(rschulterz));
                f = Math.Abs(Convert.ToInt32(rschultery));
                g = Math.Abs(Convert.ToInt32(relle));
                h = Math.Abs(Convert.ToInt32(hals));

                byte[] ArduinoBuffer = new byte[] { Convert.ToByte(a), Convert.ToByte(b), Convert.ToByte(c), Convert.ToByte(d), Convert.ToByte(e), Convert.ToByte(f), Convert.ToByte(g) , Convert.ToByte(h)};
                if (serialport.IsOpen)
                {
                    try
                    {
                        serialport.Write(ArduinoBuffer, 0, ArduinoBuffer.Length);
                    }
                    catch (Exception)
                    {
                        serialport.Close();
                    }

                   
                }
            }
        }

        //Serial wird gesetzt
        private void ArduinoSetSerial()
        {
            if (serialport.PortName != null && serialport.IsOpen)
            {
                serialport.Close();

                serialport.PortName = "COM" + ComP.Text;  
            }
            else
            {
                serialport.PortName = "COM" + ComP.Text;

            }

            serialport.BaudRate = 9600; ;
            serialport.DataBits = 8;
            serialport.Handshake = 0;
            serialport.ReadTimeout = 500;
            serialport.WriteTimeout = 500;
            
        }

        //Öffnet das Serial des Arduinos
        private void ArduinoOpenSerial()
        {
            if ((!serialport.IsOpen))
            {
                try
                {
                    serialport.Open();
                    ComP.IsEnabled = false;
                    MessageBox.Show("Serielle Verbindung ist geöffnet!");
                    ComP.IsEnabled = true;
                }
                catch (Exception)
                {
                    ComP.IsEnabled = false;
                    MessageBox.Show("Serielle Verbindung kann nicht geöffnet werden!");
                    ComP.IsEnabled = true;
                }

            }
            else
            {
                ComP.IsEnabled = false;
                MessageBox.Show("Serielle Verbindung kann nicht geöffnet werden!");
                ComP.IsEnabled = true;
            }
            continuee = true;
        }
        //Schließt das Serial vom Arduino
        private void ArduinoCloseSerial()
        {
            if(serialport.IsOpen)
            {
                try {serialport.Close(); }
                catch(Exception e)
                { MessageBox.Show(e.ToString()); }
            }
            else { MessageBox.Show("Serielle Verbindung ist geschloßen"); }
        }
        //Koordinaten werden an dem Arduino Board gesendet
        private void ArduinoSendByte(Single kinectX, Single kinectY, Single kinectZ, Single kinectJ)
        {
            var x = 0;
            var y = 0;
            var z = 0;


            if (!Double.IsNaN(kinectX)&&(!Double.IsNaN(kinectY))&&(!Double.IsNaN(kinectZ))) {
               
                byte j;
                x = Math.Abs(Convert.ToInt32(kinectX));
                if(kinectZ<0)
                { kinectZ = kinectZ * (-1); }
                y = Math.Abs(Convert.ToByte(kinectY));
                z = Math.Abs(Convert.ToByte(kinectZ));
                //z = Convert.ToByte(kinectZ);*/
                j = Convert.ToByte(kinectJ);

           
                byte[] ArduinoBuffer = new byte[] { Convert.ToByte(x), Convert.ToByte(y), Convert.ToByte(z), j};
                
                if(serialport.IsOpen)
                {

                    serialport.Write(ArduinoBuffer, 0, ArduinoBuffer.Length);
                   
                }  
            }

            
           
           
                
            
        }

        //Koordinaten werden an dem Arduino Board gesendet
        private void ArduinoSendByteNEw(double kinectX, Single kinectJ)
        {
            if(!Double.IsNaN(kinectX))
            { int x; byte j;
            int y; int z; Single sx; Single sy; //int HowOften;
            x = Math.Abs(Convert.ToInt32(kinectX));
           
            /*
            y = Math.Abs(Convert.ToByte(kinectY));
            z = Math.Abs(Convert.ToByte(kinectZ));
            //z = Convert.ToByte(kinectZ);*/
            j = Convert.ToByte(kinectJ);

            byte[] ArduinoBuffer = new byte[] { Convert.ToByte(x), j };
            if (serialport.IsOpen)
            {

                serialport.Write(ArduinoBuffer, 0, ArduinoBuffer.Length);
            } }
        }

        //Koordinaten wird dem Skelet übergeben
        private void SendToArduino(Joint joint, JointType JID)
        {
            var scaledJoint = joint.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var z = (scaledJoint.Position.Z - 1) * 100;
            ArduinoSendByte(scaledJoint.Position.X, scaledJoint.Position.Y, z, Convert.ToInt32(JID));
        }

        #endregion

        #region Membervariable für die Zeichnung
        // Breite der äußeren Zeichnung
        private const float RenderWidth = 640.0f;

        //Höhe der äußeren Zeichnung
        private const float RenderHeight = 480.0f;
       
        private const double JointThickness = 3;
        
        private const double BodyCenterThickness = 10;
        
        private const double ClipBoundsThickness = 10;
        
        private readonly Brush centerPointBrush = Brushes.Blue;
        
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
       
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);
       
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        
        private KinectSensor sensor;
        
        private DrawingGroup drawingGroup;
        
        private DrawingImage imageSource;
        #endregion

        #region Berechnungen
        //Länge zwischen zwei Joints berechnen
        private double laengeberechnung(Joint joint1, Joint joint2)
        {
            double lange;
            var scaledJoint1 = joint1.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var scaledJoint2 = joint2.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            lange = Convert.ToDouble(Math.Sqrt((Convert.ToInt32(scaledJoint1.Position.X - scaledJoint2.Position.X) ^ 2) + (Convert.ToInt32(scaledJoint1.Position.Y - scaledJoint2.Position.Y) ^ 2) + Convert.ToInt32(scaledJoint1.Position.Z - scaledJoint2.Position.Z) ^ 2));
            return lange;
        }

        // ellbogen winkel
        private double winkelellbogen(SkeletonPoint hand, SkeletonPoint schulterelle, SkeletonPoint ellehand)
        {

            var e = Math.Sqrt(Math.Pow(hand.X, 2) + Math.Pow(hand.Y, 2) + Math.Pow(hand.Z, 2));
            var w = Math.Sqrt(Math.Pow(schulterelle.X, 2) + Math.Pow(schulterelle.Y, 2) + Math.Pow(schulterelle.Z, 2));
            var s = Math.Sqrt(Math.Pow(ellehand.X, 2) + Math.Pow(ellehand.Y, 2) + Math.Pow(ellehand.Z, 2));

            var zaehler = Math.Pow(s, 2) + Math.Pow(w, 2) - Math.Pow(e, 2);
            var nenner = 2*s*w;

            double gesamt;

            if((zaehler / nenner) <= -1)
            {
                gesamt = 180;
            }
            else
            {
                gesamt = (Math.Acos(zaehler / nenner) * (180 / Math.PI));
            }
             

            return gesamt;
        }

        //kamerawinkel korektur
        private float kamerawinkelkorektur(float x)
        {
            int kamera = sensor.ElevationAngle;

            if (kamera >= 0)
            {
                x -= kamera;
                //x -= xkorekturvonell; //beachten

                if (x < 0)
                {
                    x = 0;
                }

            }
            else
            {
                x += kamera;
                //x -= xkorekturvonell; //beachten

                if (x > 180)
                {
                    x = 180;
                }
            }

            return x;
        }

        //kopf winkelkorektur
        private float winkelkorekturkopf(float x, Joint kopftop , Joint shouldercenter)
        {
            float y = 20; //vergrößerung

            if (float.IsNaN(x))
            {
                x = 90;
            }
            else
            {
                if (kopftop.Position.X > shouldercenter.Position.X)
                {
                    x += y;
                    x += 90;

                }
                else if (kopftop.Position.X < shouldercenter.Position.X)
                {
                    x += y;
                    x = 90 - x;

                }
            }
            
                return x;
            
        }

        // korrektur für x achse für arduino
        private float winkelkorekturx(float x)
        {
            x -= 20;

            if (x > 0)
            {
                return x;
            }
            else
            {
                return 0;
            }

        }

        // ueber 90 grad
        private float winkelkorektur(Joint p1, Joint p2,  Joint p3, float winkel)
        {
            if (dynVektor)
            {
                if (p1.Position.Y > p3.Position.Y)
                {
                    return 180 - winkel;
                }
                else
                {
                    return winkel;
                }
            }
            else
            {
                if (p2.Position.Y > p3.Position.Y)
                {
                    return 180 - winkel;
                }
                else
                {
                    return winkel;
                }
            }
           
        }

        //anpassung für armbewegung beim überlappenden Joints im Kinect
        //private float xkorekturvonell = 0;
        private bool dynVektor;
        private SkeletonPoint DynVektor(SkeletonPoint sp1, SkeletonPoint sp2 , float ellwinkel)
        {
            int grenze = 160;

            SkeletonPoint sp = sp1;
            //xkorekturvonell = 0;
            dynVektor = false;
      
                    if (ellwinkel >= grenze)
                    {
                        //ellwinkel = 180 - ellwinkel;
                        //xkorekturvonell = ellwinkel;
                        sp = sp2;
                        dynVektor = true;
                    }
                   
                  
            return sp;
        }


        //Vektorenberechnung
        private SkeletonPoint VektorAusZweiPunkten(Joint p1, Joint p2, string Richtung)
        {
            var vektorz = 0;
            var vektorx = 0;
            var vektory = 0;
            var scaledJoint1 = p1.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var z1 = scaledJoint1.Position.Z * ScreenMaxX;
            var scaledJoint2 = p2.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var z2 = scaledJoint2.Position.Z * ScreenMaxX;

            switch (Richtung)
            {
                case "xy":

                    vektorx = Convert.ToInt32(scaledJoint1.Position.X.Absltflo() - scaledJoint2.Position.X.Absltflo());
                    vektory = Convert.ToInt32(scaledJoint1.Position.Y.Absltflo() - scaledJoint2.Position.Y.Absltflo());
                    break;
                case "xz":
                    vektorx = Convert.ToInt32(scaledJoint1.Position.X.Absltflo() - scaledJoint2.Position.X.Absltflo());
                    vektorz = Convert.ToInt32(z1.Absltflo() - z2.Absltflo());
                    break;
                case "yz":
                    vektory = Convert.ToInt32(scaledJoint1.Position.Y.Absltflo() - scaledJoint2.Position.Y.Absltflo());
                    vektorz = Convert.ToInt32(z1.Absltflo() - z2.Absltflo());
                    break;
                case "xyz":
                    //vektorx = Convert.ToInt32(scaledJoint1.Position.X - scaledJoint2.Position.X);
                    //vektory = Convert.ToInt32(scaledJoint1.Position.Y - scaledJoint2.Position.Y);
                    //vektorz = Convert.ToInt32(scaledJoint1.Position.Z - scaledJoint2.Position.Z);
                    vektorx = Convert.ToInt32((p1.Position.X * 100) - (p2.Position.X * 100));
                    vektory = Convert.ToInt32((p1.Position.Y * 100) - (p2.Position.Y * 100));
                    vektorz = Convert.ToInt32((p1.Position.Z * 100) - (p2.Position.Z * 100));
                    break;
            }

            SkeletonPoint vektorxyz = new SkeletonPoint();
            vektorxyz.X = vektorx;
            vektorxyz.Y = vektory;
            vektorxyz.Z = vektorz;
            return vektorxyz;
        }

        //alternativ winkelberechnung x
        private double WinkelBerechnungX(SkeletonPoint V1)
        {
            double gesamt = 0;
            double Zaehler = 0;
            double nenner = 0;

            Zaehler = V1.X;
            nenner = Math.Sqrt(Math.Pow(V1.X, 2) + Math.Pow(V1.Y, 2) + Math.Pow(V1.Z, 2));
            gesamt = Math.Asin(Zaehler.Absltdou()/nenner.Absltdou())*(180/Math.PI);

            //wenn zähler und nenner null sind 
            if (Double.IsNaN(gesamt))
            {
                gesamt = 0;
            }

            return gesamt;

        }

        //Winkelberechnung
        private double Winkelberechnung(SkeletonPoint V1, SkeletonPoint V2, string Richtung)
        {
            double gesamt = 0;
            double Zaehler = 0;
            double nenner1 = 0;
            double nenner2 = 0;
            switch (Richtung)
            {
                case "xy":
                    Zaehler = (V1.X * V2.X) + (V1.Y * V2.Y);
                    nenner1 = Math.Sqrt((V1.X * V1.X) + (V1.Y * V1.Y));
                    nenner2 = Math.Sqrt((V2.X * V2.X) + (V2.Y * V2.Y));
                    break;
                case "xz":
                    Zaehler = (V1.X * V2.X) + (V1.Z * V2.Z);
                    nenner1 = Math.Sqrt((V1.X * V1.X) + (V1.Z * V1.Z));
                    nenner2 = Math.Sqrt((V2.X * V2.X) + (V2.Z * V2.Z));
                    break;
                case "yz":
                    Zaehler = (V1.Y * V2.Y) + (V1.Z * V2.Z);
                    nenner1 = Math.Sqrt((V1.Y * V1.Y) + (V1.Z * V1.Z));
                    nenner2 = Math.Sqrt((V2.Y * V2.Y) + (V2.Z * V2.Z));
                    break;
                case "xyz":
                    Zaehler = (V1.X * V2.X) + (V1.Y * V2.Y) + (V1.Z * V2.Z);
                    nenner1 = Math.Sqrt(Math.Pow(V1.X, 2) + Math.Pow(V1.Y, 2) + Math.Pow(V1.Z, 2));
                    nenner2 = Math.Sqrt(Math.Pow(V2.X, 2) + Math.Pow(V2.Y, 2) + Math.Pow(V2.Z, 2));
                    break;
            }

            var nenner = nenner1 * nenner2;
            gesamt = (Math.Acos(Zaehler.Absltdou() / nenner.Absltdou()) * (180 / Math.PI));

            //wenn zähler und nenner null sind 
            if (Double.IsNaN(gesamt))
            {
                gesamt = 0;
            }
            
            return gesamt;

          
        }

        private double cosinuwinkelberechnung(double a, double b, double c)
        {
            double winkel = 0;
            if ((!Double.IsNaN(a)) || (!Double.IsNaN(b) || (!Double.IsNaN(b))))
            {
                double zahl = ((a * a) + (b * b) - (c * c)) / (2 * a * b);
                winkel = Math.Acos(zahl);
                winkel = winkel * 57.29577951;
            }
            return winkel;
        }

        //winkelberechnung für z achse
        private float formelzy(float ez, float ey, float sz, float sy)
        {
                double a = Convert.ToInt32(ez - sz);
                double b = Convert.ToInt32(ey - sy);
                double radian = Math.Atan2(a.Absltdou() , b.Absltdou());

                return Convert.ToSingle(radian * (180 / Math.PI));
           
        }

        private float zwinkel(Joint schulter, Joint ellbogen)
        {
            float sx = schulter.Position.X.Absltflo();
            float sy = schulter.Position.Y.Absltflo();
            float sz = schulter.Position.Z.Absltflo();

            float ex = ellbogen.Position.X.Absltflo();
            float ey = ellbogen.Position.Y.Absltflo();
            float ez = ellbogen.Position.Z.Absltflo();


            if (sz >= ez)
            {
                if (sy > ey)
                {
                    if (sz <= ez)
                    {
                        return 0;
                    }
                    else
                    {
                        //formel zy
                        return formelzy(ez, ey, sz, sy);
                    }
                    
                }
                else if (sy < ey)
                {
                    if (sz <= ez)
                    {
                        if (sx >= ex)
                        {
                            //formel zy 180
                            return 180 - formelzy(ez, ey, sz, sy);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        //formel zy 180
                        return 180 - formelzy(ez, ey, sz, sy);
                    }
                }
                else
                {
                    if (sx >= ex)
                    {
                        return 90;
                    }
                    else
                    {
                        if (sz <= ez)
                        {
                            return 0;
                        }
                        else
                        {
                            return 90;
                        }
                    }
                }

            }
            else
            {
                return 0;
            }
            
        }

        //Wineklberechnung für x-achse 
        private float formelxy(float ex, float ey, float sx, float sy)
        {
            double a = Convert.ToInt32(ex - sx);
            double b = Convert.ToInt32(ey - sy);
            double radian = Math.Atan2(a.Absltdou(), b.Absltdou());

            return Convert.ToSingle(radian * (180 / Math.PI));

        }

        private float formelxz(float ex, float ez, float sx, float sz)
        {
            double a = Convert.ToInt32(ex - sx);
            double b = Convert.ToInt32(ez - sz);
            double radian = Math.Atan2(a.Absltdou(), b.Absltdou());

            return Convert.ToSingle(radian * (180 / Math.PI));

        }

        private float formelxyz(float ex, float ey, float ez, float sx, float sy, float sz)
        {
            double x = Convert.ToInt32(ex - sx);
            double y = Convert.ToInt32(ey - sy);

            double a = Math.Sqrt((Math.Pow(x.Absltdou(), 2) + (Math.Pow(y.Absltdou(), 2))));
            double b = Convert.ToInt32(ez - sz);
            double radian = Math.Atan2(a.Absltdou(), b.Absltdou());

            return Convert.ToSingle(radian * (180 / Math.PI));

        }

        private float xwinkel(Joint schulter, Joint ellbogen)
        {
            float sx = schulter.Position.X.Absltflo();
            float sy = schulter.Position.Y.Absltflo();
            float sz = schulter.Position.Z.Absltflo();

            float ex = ellbogen.Position.X.Absltflo();
            float ey = ellbogen.Position.Y.Absltflo();
            float ez = ellbogen.Position.Z.Absltflo();

            if (sx < ex)
            {
                if (sy > ey)
                {
                    if (sz <= ez)
                    {
                        //formel xy
                        return formelxy(ex, ey, sx, sy);
                    }
                    else
                    {
                        //formel xyz
                        return formelxyz(ex, ey, ez, sx, sy, sz);
                    }
                    
                }
                else if (sy < ey)
                {

                    if (sz <= ez)
                    {
                        //formel xy 180
                        return 180 - formelxy(ex, ey, sx, sy);

                    }
                    else
                    {
                        //formel xyz
                        return formelxyz(ex, ey, ez, sx, sy, sz);
                    }

                }
                else
                {
                    if (sz <= ez)
                    {
                        return 90;
                    }
                    else
                    {
                        //formel xz
                        return formelxz(ex, ez, sx, sz);
                    }
                }
            }
            else
            {
                return 0;
            }

        }
        #endregion

        //Zeichnet Knochen und Joints //Übergabe ans Arduino
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            

            #region Zeichnung
            // Bereiche des Körpers defnieren
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);


            // Linker Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Rechter Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);


            // Linkes Bein
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Rechtes Bein
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);
            #endregion

            #region Koordinaten werden an Arduino gesendet


            #region Zweite Methode

            //Checkbox überprüfen
            bool? sw = schulterwrist.IsChecked;
            bool _sw = sw ?? false;

            bool? sn = sinus.IsChecked;
            bool _sn = sn ?? false;

            #region Linke Arm

            tb_x_ell.Text = skeleton.Joints[JointType.ElbowLeft].Position.X.ToString();
            tb_y_ell.Text = skeleton.Joints[JointType.ElbowLeft].Position.Y.ToString();
            tb_z_ell.Text = skeleton.Joints[JointType.ElbowLeft].Position.Z.ToString();

            tb_x_schulter.Text = skeleton.Joints[JointType.ShoulderLeft].Position.X.ToString();
            tb_y_schulter.Text = skeleton.Joints[JointType.ShoulderLeft].Position.Y.ToString();
            tb_z_schulter.Text = skeleton.Joints[JointType.ShoulderLeft].Position.Z.ToString();

            tb_x_sc.Text = skeleton.Joints[JointType.ShoulderCenter].Position.X.ToString();
            tb_y_sc.Text = skeleton.Joints[JointType.ShoulderCenter].Position.Y.ToString();
            tb_z_sc.Text = skeleton.Joints[JointType.ShoulderCenter].Position.Z.ToString();

            tb_x_sp.Text = skeleton.Joints[JointType.Spine].Position.X.ToString();
            tb_y_sp.Text = skeleton.Joints[JointType.Spine].Position.Y.ToString();
            tb_z_sp.Text = skeleton.Joints[JointType.Spine].Position.Z.ToString();


            //Linker Ellenbogen
            SkeletonPoint SchulterElle = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "xyz");
            SkeletonPoint ElleHand = VektorAusZweiPunkten(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.ElbowLeft], "xyz");
            SkeletonPoint Hand = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderLeft], skeleton.Joints[JointType.WristLeft], "xyz");
            float winkelElleLinks = Convert.ToSingle(winkelellbogen(Hand, SchulterElle, ElleHand));
           
            tb_ellbogen.Text = winkelElleLinks.ToString();

            //Schulter links in Z Richtung
            SkeletonPoint SchulterWristZ = VektorAusZweiPunkten(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.ShoulderLeft], "yz");
            SkeletonPoint SchulterElleZ = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "yz");
            SkeletonPoint ShoulterSpline = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "yz");
            
            float winkelShulterLinks = 0;
            if (_sw)
            {
                SkeletonPoint SchulterZ = DynVektor(SchulterElleZ, SchulterWristZ, winkelElleLinks);

                winkelShulterLinks = Convert.ToSingle(Winkelberechnung(SchulterZ, ShoulterSpline, "yz")); //dynvektor beutzen, ersetze SchulterElleZ durch  SchulterZ
               
            }
            else
            {
                winkelShulterLinks = Convert.ToSingle(Winkelberechnung(SchulterElleZ, ShoulterSpline, "yz")); //dynvektor beutzen, ersetze SchulterElleZ durch  SchulterZ
               
            }

            winkelShulterLinks = winkelkorektur(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.ElbowLeft],
                  skeleton.Joints[JointType.ShoulderLeft], winkelShulterLinks); 
            //korektur neigung der kamera , wenn kamera niedrig  kamerawinkel + dann winkel -
            winkelShulterLinks = kamerawinkelkorektur(winkelShulterLinks);

            tb_zyrichtung.Text = winkelShulterLinks.ToString();

            //Schulter links in xy Richtung
            SkeletonPoint SchulterWristXY = VektorAusZweiPunkten(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.ShoulderLeft], "xy");
            SkeletonPoint SchulterElleXY = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "xy");
            SkeletonPoint ShoulterSplinexy = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "xy");

            float winkelShulterLinksxy = 0;
            if (_sw)
            {
                SkeletonPoint SchulterXY = DynVektor(SchulterElleXY, SchulterWristXY, winkelElleLinks);

                if (_sn)
                {
                    winkelShulterLinksxy = Convert.ToSingle(WinkelBerechnungX(SchulterXY));
                }
                else
                {
                    winkelShulterLinksxy = Convert.ToSingle(Winkelberechnung(SchulterXY, ShoulterSplinexy, "xy")); //dynvektor beutzen, ersetze SchulterElleZ durch  SchulterZ
                }
               
                
            }
            else
            {
                winkelShulterLinksxy = Convert.ToSingle(_sn ? WinkelBerechnungX(SchulterElleXY) : Winkelberechnung(SchulterElleXY, ShoulterSplinexy, "xy"));
            }

            // korektur für arduino , damit nicht so sensible ist
            winkelShulterLinksxy = winkelkorekturx(winkelShulterLinksxy);
           
            tb_xyrichtung.Text = winkelShulterLinksxy.ToString();
            
            #endregion
            
            #region Rechter Arm


            //Rechter Ellenbogen
            SkeletonPoint SchulterEllerechts = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "xyz");
            SkeletonPoint ElleHandrechts = VektorAusZweiPunkten(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ElbowRight], "xyz");
            SkeletonPoint Handrecht = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.WristRight], "xyz");
            float winkelElleRechts = Convert.ToSingle(winkelellbogen(Handrecht, SchulterEllerechts, ElleHandrechts));
           
            tb_ellbogen_r.Text = winkelElleRechts.ToString();
            
            //Schulter links in Z Richtung
            SkeletonPoint SchulterWristZRechts = VektorAusZweiPunkten(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ShoulderRight], "yz");
            SkeletonPoint SchulterElleZRechts = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "yz");

            float winkelShulterRechts = 0;
            if (_sw)
            {
                SkeletonPoint SchulterZRechts = DynVektor(SchulterElleZRechts, SchulterWristZRechts, winkelElleRechts);

                winkelShulterRechts = Convert.ToSingle(Winkelberechnung(SchulterZRechts, ShoulterSpline, "yz")); //dynvektor beutzen, ersetze SchulterElleZ durch  SchulterZ
           
            }
            else
            {
                winkelShulterRechts = Convert.ToSingle(Winkelberechnung(SchulterElleZRechts, ShoulterSpline, "yz")); //dynvektor beutzen, ersetze SchulterElleZ durch  SchulterZ
           
            }
           
            winkelShulterRechts = winkelkorektur(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ElbowRight],
            skeleton.Joints[JointType.ShoulderRight], winkelShulterRechts); 
            //kamerawinkel korrektur 
            winkelShulterRechts = kamerawinkelkorektur(winkelShulterRechts);

            tb_zyrichtung_r.Text = winkelShulterRechts.ToString();
           
            //Schulter links in xy Richtung
            SkeletonPoint SchulterWristZRechtsXY = VektorAusZweiPunkten(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ShoulderRight], "xy");
            SkeletonPoint SchulterEllerechtsXY = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "xy");

            float winkelShulterLinksxyRechts = 0;
            if (_sw)
            {
                SkeletonPoint SchulterXYRechts = DynVektor(SchulterEllerechtsXY, SchulterWristZRechtsXY, winkelElleRechts);

                winkelShulterLinksxyRechts = Convert.ToSingle(_sn ? WinkelBerechnungX(SchulterXYRechts) : Winkelberechnung(SchulterXYRechts, ShoulterSplinexy, "xy"));
            }
            else
            {
                winkelShulterLinksxyRechts = Convert.ToSingle(_sn ? WinkelBerechnungX(SchulterEllerechtsXY) : Winkelberechnung(SchulterEllerechtsXY, ShoulterSplinexy, "xy"));
            }

            //korektur für arduino 
            winkelShulterLinksxyRechts = winkelkorekturx(winkelShulterLinksxyRechts);
            
            tb_xyrichtung_r.Text = winkelShulterLinksxyRechts.ToString();



            #endregion

            #region Hals
            /*Console.Write(Kopf_ShoulderCenter.X +" " + Kopf_ShoulderCenter.Y + " " + Kopf_ShoulderCenter.Z + "\n" +
                ShouldereCenter_Spine.X + " " + ShouldereCenter_Spine.Y + " " + ShouldereCenter_Spine.Z);*/
            //tb_kopf.Text = winkelHals.ToString();


            var a1 = new Vector(skeleton.Joints[JointType.Head].Position.X, skeleton.Joints[JointType.Head].Position.Y, skeleton.Joints[JointType.Head].Position.Z);
            var a2 = new Vector(skeleton.Joints[JointType.ShoulderCenter].Position.X, skeleton.Joints[JointType.ShoulderCenter].Position.Y, skeleton.Joints[JointType.ShoulderCenter].Position.Z);
            var a3 = new Vector(skeleton.Joints[JointType.Spine].Position.X, skeleton.Joints[JointType.Spine].Position.Y, skeleton.Joints[JointType.Spine].Position.Z);

            Vector b1 = new Vector(a3.X - a1.X, a3.Y - a1.Y, a3.Z - a1.Z);
            Vector b2 = new Vector(a2.X - a1.X, a2.Y - a1.Y, a2.Z - a1.Z);
            double b1Betrag = Math.Sqrt(Math.Pow(b1.X, 2) + Math.Pow(b1.Y, 2) + Math.Pow(b1.Z, 2));
            double b2Betrag = Math.Sqrt(Math.Pow(b2.X, 2) + Math.Pow(b2.Y, 2) + Math.Pow(b2.Z, 2));
            double SkalarProdukt = b1.X * b2.X + b1.Y * b2.Y + b1.Z * b2.Z;
            double preAcos = SkalarProdukt / (b1Betrag * b2Betrag);
            double afterAcos = Math.Acos(preAcos) * 150;
            //linkes Bein
            var c1 = new Vector(skeleton.Joints[JointType.AnkleRight].Position.X, skeleton.Joints[JointType.AnkleRight].Position.Y, skeleton.Joints[JointType.AnkleRight].Position.Z);
            var c2 = new Vector(skeleton.Joints[JointType.KneeRight].Position.X, skeleton.Joints[JointType.KneeRight].Position.Y, skeleton.Joints[JointType.KneeRight].Position.Z);
            var c3 = new Vector(skeleton.Joints[JointType.HipRight].Position.X, skeleton.Joints[JointType.HipRight].Position.Y, skeleton.Joints[JointType.HipRight].Position.Z);

            Vector d1 = new Vector(c3.X - c1.X, c3.Y - c1.Y, c3.Z - c1.Z);
            Vector d2 = new Vector(c2.X - c1.X, c2.Y - c1.Y, c2.Z - c1.Z);
            double d1Betrag = Math.Sqrt(Math.Pow(d1.X, 2) + Math.Pow(d1.Y, 2) + Math.Pow(d1.Z, 2));
            double d2Betrag = Math.Sqrt(Math.Pow(d2.X, 2) + Math.Pow(d2.Y, 2) + Math.Pow(d2.Z, 2));
            double SkalarProduktBein = d1.X * d2.X + d1.Y * d2.Y + d1.Z * d2.Z;
            double preAcosBein = SkalarProduktBein / (d1Betrag * d2Betrag);
            double afterAcosBein = Math.Acos(preAcosBein) * 150;

            c1 = new Vector(skeleton.Joints[JointType.AnkleLeft].Position.X, skeleton.Joints[JointType.AnkleLeft].Position.Y, skeleton.Joints[JointType.AnkleLeft].Position.Z);
            c2 = new Vector(skeleton.Joints[JointType.KneeLeft].Position.X, skeleton.Joints[JointType.KneeLeft].Position.Y, skeleton.Joints[JointType.KneeLeft].Position.Z);
            c3 = new Vector(skeleton.Joints[JointType.HipLeft].Position.X, skeleton.Joints[JointType.HipLeft].Position.Y, skeleton.Joints[JointType.HipLeft].Position.Z);

            d1 = new Vector(c3.X - c1.X, c3.Y - c1.Y, c3.Z - c1.Z);
            d2 = new Vector(c2.X - c1.X, c2.Y - c1.Y, c2.Z - c1.Z);
            d1Betrag = Math.Sqrt(Math.Pow(d1.X, 2) + Math.Pow(d1.Y, 2) + Math.Pow(d1.Z, 2));
            d2Betrag = Math.Sqrt(Math.Pow(d2.X, 2) + Math.Pow(d2.Y, 2) + Math.Pow(d2.Z, 2));
            SkalarProduktBein = d1.X * d2.X + d1.Y * d2.Y + d1.Z * d2.Z;
            preAcosBein = SkalarProduktBein / (d1Betrag * d2Betrag);
            double afterAcosBeinlefty = Math.Acos(preAcosBein) * 150;

            float winkel_beinl = 180 - Convert.ToSingle(afterAcosBeinlefty) - 90;
            float winkel_beinr = 180 - Convert.ToSingle(afterAcosBein) - 90;
            float winkel_bein = (winkel_beinl + winkel_beinr) / 2;



            #endregion
            float winkelHals = Convert.ToSingle(afterAcos);

            #region Kopf
            //SendToArduino(skeleton.Joints[JointType.Head], JointType.Head);
            SkeletonPoint kopfTop = VektorAusZweiPunkten(skeleton.Joints[JointType.Head], skeleton.Joints[JointType.ShoulderCenter], "xyz");
            SkeletonPoint koerper = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "xyz");
            SkeletonPoint koerTop = VektorAusZweiPunkten(skeleton.Joints[JointType.Head], skeleton.Joints[JointType.Spine], "xyz");

            //richtung bestimmen mit x postion vom kopf vom schulter center 
            //winkel ausgeben 90 + für rechts , 90 - für links
            float winkelKopf = Convert.ToSingle(winkelellbogen(kopfTop , koerTop, koerper));
            winkelKopf = winkelkorekturkopf(winkelKopf, skeleton.Joints[JointType.Head], skeleton.Joints[JointType.ShoulderCenter]);

            tb_kopf.Text = winkelKopf.ToString();

            #endregion

            #region send to arduino
            // ArduinoSendByteNEw(winkelElleLinks, winkelShulterLinksxy, winkelShulterLinks, winkelkopf, winkelShulterRechts, winkelShulterLinksxyRechts, winkelElleRechts);
            ArduinoSendByteNEw(winkelElleLinks, winkelShulterLinksxy, winkelShulterLinks, winkelKopf, winkelShulterRechts, winkelShulterLinksxyRechts, winkelElleRechts, winkelHals);

            #endregion



            #endregion

            #endregion

            #region Kalibrierung der Zeichnung
            // Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }
            #endregion
        }

        #region Zusatzfunktionen
        //Hauptfunktion
        public MainWindow()
        {
            InitializeComponent();
        }
       
        //Funktion für die Zeichnung
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new System.Windows.Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new System.Windows.Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new System.Windows.Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new System.Windows.Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

        //Starten der Aufgaben
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.drawingGroup = new DrawingGroup();

            // Bildquelle wird erzeugt
            this.imageSource = new DrawingImage(this.drawingGroup);

            //  Zeichnung wird angezeigt
            Image.Source = this.imageSource;

            // listen to any status change for Kinects.
            //KinectSensor.KinectSensors.StatusChanged += this.KinectsStatusChanged;

            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }

              
            }

           
            
        }

        //event handler, wenn kinect verbunden oder nicht .
        private void KinectsStatusChanged(object sender, StatusChangedEventArgs e)
        {
           

            if (e.Status == KinectStatus.Connected)
            {
                this.sensor = e.Sensor;

            }
            else
            {
                this.sensor = null;
                
            }

            

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }


            }
        }


        //Schließt das Fenster
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }
        
        // Event handler für Kinect Sensor's SkeletonFrameReady event
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.AliceBlue, null, new System.Windows.Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        RenderClippedEdges(skel, dc);

                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {

                            //this.DrawBonesAndJoints(skel, dc);

                            if (skel.Position.Z < closestDistance)
                            {
                                closestID = skel.TrackingId;
                                closestDistance = skel.Position.Z;
                            }
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                                this.centerPointBrush,
                                null,
                                this.SkeletonPointToScreen(skel.Position),
                                BodyCenterThickness,
                                BodyCenterThickness);
                        }
                        
                    }

                    if (closestID > 0)
                    {
                        try
                        {
                            Skeleton skel = skeletons.First(s => s.TrackingId == closestID);



                            this.DrawBonesAndJoints(skel, dc);

                        }
                        catch (Exception)
                        {
                            closestID = 0;
                            closestDistance = 5f;
                        }
                      
                    }



                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new System.Windows.Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }
        
        //Funktion ob der ganze Körper dargestellt wird
        //private void CheckBoxSeatedModeChanged(object sender, RoutedEventArgs e)
        //{
        //    if (null != this.sensor)
        //    {
        //        if (this.checkBoxSeatedMode.IsChecked.GetValueOrDefault())
        //        {
        //            this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
        //        }
        //        else
        //        {
        //            this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
        //        }
        //    }
        //}
        
        //Skeletpunkte wird dem Bildschirm übergeben
        private System.Windows.Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new System.Windows.Point(depthPoint.X, depthPoint.Y);
        }
        

        //Ein Knochen wird zwischen zwei Joints gezeichnet
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }
        #endregion

        //eventhandler für com 
        private void ComP_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (ComP.Text != string.Empty)
                {
                    ArduinoSetSerial();
                    ArduinoOpenSerial();
                }
            }

        }

    }
}