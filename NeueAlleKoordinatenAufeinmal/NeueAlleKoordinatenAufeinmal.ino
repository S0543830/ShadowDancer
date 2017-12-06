
#include <Servo.h>

Servo lelle; 
Servo lschultery; 
Servo lschulterz; 
Servo rschultery;
Servo rschulterz; 
Servo relle;
Servo kopf;
void setup()
{
  Serial.begin(9600);

  lelle.attach(31);
  lschultery.attach(33);
  lschulterz.attach(35);
  
  kopf.attach(22);
  
  rschulterz.attach(30);
  rschultery.attach(32);
  relle.attach(34);
//------------------------------------------------------------------------------------------------\\  
  lelle.write(150);
  lschultery.write(0);
  lschulterz.write(0);
  
  kopf.write(0);
  
  rschulterz.write(170); 
  rschultery.write(150);
  relle.write(60);
}

unsigned char a,b,c,d,e,f,g =0;

void loop()
{
  if (Serial.available() >= 7) {
    a = Serial.read();       
    b = Serial.read();   
    c = Serial.read();  
    d = Serial.read();  
    e = Serial.read();   
    f = Serial.read();  
    g = Serial.read();  
/*
lelle.write(a);
lschultery.write(b);
lschulterz.write(c);
kopf.write(d);
rschulterz.write(170-e);
rschultery.write(150-f);
relle.write(105-g);
   */
   lelle.write(g);

lschultery.write(f);

lschulterz.write(e);

kopf.write(d);

rschulterz.write(170-c);

rschultery.write(150-b);

relle.write(105-a);

  }
}








