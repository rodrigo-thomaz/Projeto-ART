/*   
  >>>>> Criando Robô Com Arduino <<<<<   
  ##### Robôs, Projetos e Tutoriais! #####  
  .:: Site principal: http://www.CriandoRoboComArduino.com/     
  .:: Site secundário: http://www.TestCriandoRoboComArduino.com/  
  ========== IMPORTANTE ==========   
  O código está livre para usar, citar, alterar e compartilhar,  
  desde que mantenha o site como referência.   
  Obrigado.  
  --------------------------------------------------------------------------------------------------  
  Projeto: Como usar o buzzer (som) no arduíno    
  ---------------------------------------------------------------------------------------------------  
 */   
  int BUZZER = 13; // Ligar o buzzer (Som) no pino 10   
  // Executado na inicialização do Arduino   
  void setup(){   

    Serial.begin(9600);
    
    pinMode(BUZZER,OUTPUT); // define o pino do buzzer como saída.   
    
  }   
  // Loop pincipal do Arduino   
  void loop(){   

    Serial.println("passando 300");
    tone(BUZZER,300,300); //aqui sai o som   
  /*   
   o número 10 indica que o pino positivo do buzzer está na porta 10   
   o número 300 é a frequência que será tocado   
   o número 300 é a duração do som   
  */    
    delay(500);    
    Serial.println("passando 100");
    tone(BUZZER,100,300); //aqui sai o som   
    delay(500);   
    Serial.println("passando 900");
    tone(BUZZER,900,300); //aqui sai o som   
    delay(500);   
  }   


  //http://www.criandorobocomarduino.com/2013/09/como-usar-o-buzzer-som-no-arduino.html
