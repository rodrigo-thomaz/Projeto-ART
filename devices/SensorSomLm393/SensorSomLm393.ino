//Programa: Sensor de som KY-038
//Autor: Arduino e Cia


//Definicao pinos sensor
int pino_analogico = 15;
int pino_digital = 13;

int valor_A0 = 0;
int valor_D = 0;

void setup()
{
  Serial.begin(9600);
  
  //Define pinos sensor como entrada
  pinMode(pino_analogico, INPUT);
  pinMode(pino_digital, INPUT);
}

void loop()
{
  valor_A0 = analogRead(pino_analogico);
  valor_D = digitalRead(pino_digital);
  Serial.print("Saida A0: ");
  Serial.print(valor_A0);
  Serial.print(" Saida D0: ");
  Serial.println(valor_D);
  
  //delay(50);
}
