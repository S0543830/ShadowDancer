#include <Servo.h>
Servo rechterEllenbogen;
Servo linkerEllenbogen;
Servo rschultery;
Servo rschulterz; 
Servo lschultery; 
Servo lschulterz; 
Servo kopfy;
Servo kopfz;
void setup() {
   Serial.begin(9600);
  rechterEllenbogen.attach(7);
  rechterEllenbogen.write(20);

  rschultery.attach(13);
  rschultery.write(15);
  
  rschulterz.attach(12);
  rschulterz.write(0);

  kopfy.attach(10);
  kopfy.write(130);

  kopfz.attach(11);
  kopfz.write(170);
  lschulterz.attach(9);
  lschulterz.write(180); 
  
  lschultery.attach(8);
  lschultery.write(180);

  linkerEllenbogen.attach(6);
  linkerEllenbogen.write(170);
}
unsigned char a,b,c,d,e,f,g,h =0;
void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() >= 8) {
    a = Serial.read();   //linker Ellenbogen    
    b = Serial.read();   //linke Schultery
    c = Serial.read();  //linke Schulter z
    d = Serial.read();  //Kopfy
    e = Serial.read(); //rechte Schulter z
    f = Serial.read();  //rechte Schulter y
    g = Serial.read();  //rechter Ellenbogen
    h = Serial.read(); //Kopfz
    rechterEllenbogen.write(180-g); 
    rschulterz.write(e);
    rschultery.write(f);
  
    kopfy.write(220-d);
    kopfz.write(180-h);
    
    lschulterz.write(180-c);
    lschultery.write(180-b);
    linkerEllenbogen.write(10+a); 
  }  
  
}
