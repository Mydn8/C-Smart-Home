#include <Servo.h>
Servo servoNesnesi;
#include <DHT.h>

int led=10;
int led2=8;
String x;
int val;
String x2;
int val2;
int pwm,pwm2;
String x3;
int val3;

int dc=11;
int hiz;
int pirPin = 2; // PIR pin
int deger = 0;
/////////////
int buzzer=12;
String a;
////////////7
int ldr =A1;
//////////
int trigPin = 5, echoPin = 6;
int uzaklik;
int sure;
//////////////7
#define DHTPIN 3             //DHT pin tanımlaması
#define DHTTYPE DHT11        //DHT modeli tanımlaması
DHT dht(DHTPIN, DHTTYPE);

Servo servomotor;

void setup() {
 
  pinMode(pirPin, INPUT); 
  pinMode(dc, OUTPUT);
  pinMode(led, OUTPUT);
  pinMode(led2, OUTPUT);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(buzzer, OUTPUT);
  servomotor.attach(9);
  pinMode(dc, OUTPUT);
  Serial.begin(9600);
  dht.begin();
}

void loop() {
 
  deger = digitalRead(pirPin); // Dijital pin okunuyor
  Serial.print(deger);
  Serial.print("/");      
  delayMicroseconds(30000);
/////////////////////

  int deger2 = analogRead(ldr);  //ldr'den gelen değeri oku
  Serial.print(deger2);        //değeri seri port ekranına yaz
  delayMicroseconds(30000);
  Serial.print("/");  

   int h = dht.readHumidity();           //Nem değerini oku
   int t = dht.readTemperature();        //Sıcaklık değerini oku
   Serial.print(h);            //Nem değerini seri porta gönder
   Serial.print("/");  
   Serial.print(t);            //Sıcaklık değerini seri porta gönder
   Serial.print("/");  
   delayMicroseconds(30000);
  
   digitalWrite(trigPin, LOW);
   delayMicroseconds(5);
   digitalWrite(trigPin, HIGH);
   delayMicroseconds(10);
   digitalWrite(trigPin, LOW);
   sure = pulseIn(echoPin, HIGH);
   uzaklik = sure / 2 / 29.1;
   if(uzaklik > 200)
   uzaklik = 200;
   Serial.println(uzaklik);
   
   delayMicroseconds(9985);

   ///////////////////////////////////
  if(Serial.available())
 {
  char c=Serial.read();

  if(c=='1')
{
  tone(buzzer,100);


}

  else if(c=='2')
{
  noTone(buzzer);
 
}
else if(c=='7')
{

  analogWrite(dc, 150);
  
}
else if(c=='3')
{
  analogWrite(dc, 250);
}
else if(c=='4')
{
  analogWrite(dc, 0);
}
else if(c=='5')
{
   digitalWrite(led,HIGH);
}

else if(c=='6')
{
  digitalWrite(led,LOW);
}

else if(c=='8')
{
   servomotor.write(0); 
}
else if(c=='9')
{
   servomotor.write(90); 
}
else if(c=='A')
{
   digitalWrite(led2,HIGH); 
}
else if(c=='B')
{
   digitalWrite(led2,LOW); 
}

 }

 




}
