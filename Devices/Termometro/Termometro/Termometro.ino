/*
  Termometro - Library for Termometro code.
  Created by Rodrigo Thomaz, Sep 13, 2017.
  Released into the public domain.
*/

// OneWire DS18S20, DS18B20, DS1822 Temperature Example
//
// http://www.pjrc.com/teensy/td_libs_OneWire.html
//
// The DallasTemperature library can do all this work for you!
// http://milesburton.com/Dallas_Temperature_Control_Library

#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <EEPROM.h>

#include <OneWire.h>

#include <Settings.h>

#include <TemperatureSensorManager.h>

#define MEM_ALOC_SIZE 4

// Display start

#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define OLED_RESET 0
Adafruit_SSD1306 display(OLED_RESET);

#if (SSD1306_LCDHEIGHT != 64)
#error("Height incorrect, please fix Adafruit_SSD1306.h!");
#endif

// Display end


const char* host = "Termometro";
const char* ssid = "RThomaz";
const char* password = "2919517400";

IPAddress ip(192, 168, 1, 177);
IPAddress gateway(192,168,1,1); 
IPAddress subnet(255,255,255,0); 

ESP8266WebServer server(80);

TemperatureSensorManager temperatureSensorManager;  

static const char PROGMEM INDEX_HTML[] = R"rawliteral(
<!DOCTYPE html>
<html>
<head>
    <style>
        /* Center the loader */
        .loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 150px;
            height: 150px;
            margin: -75px 0 0 -75px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            width: 120px;
            height: 120px;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
            display:none;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }        
    </style>
    <title></title>
  <meta charset='utf-8' />
</head>
<body>
    <form id="form" action='' title='Lâmpada'>
        <fieldset>
            <legend>Lâmpada</legend>
            <input type='radio' name='optLamp' value='true'> Ligado<br>
            <input type='radio' name='optLamp' value='false'> Desligado
        </fieldset>       
    </form>
    <div class="loader"></div>
</body>
</html>

<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>

<script type='text/javascript'>

    var url = 'http://192.168.1.177/';

    jQuery.ajaxSetup({
        beforeSend: function () {
            $('.loader').show();
            $('#form').hide();
        },
        complete: function () {
            $('.loader').hide();
            $('#form').show();
        },
        success: function () { }
    });

    $(document).ready(function () {

        $.ajax({
            url: url + 'getCurrent',
            type: 'get',
            data: null,
            success: function (response) {
                response = convertToJSON(response);
                $("[name=optLamp]").val([response.lamp]);
                $("[name=optFanDirection]").val([response.fanDirection]);
                $('#rngFanSpeed').val(response.fanSpeed);
            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });

        $('input[type=radio][name=optLamp]').change(function () {
            var data = 'value=' + this.value;
            $.ajax({
                url: url + 'setPowerLamp',
                type: 'post',
                data: data,
                success: function (response) {
                    //Do Something                    
                },
                error: function (xhr) {
                    //Do Something to handle error
                }
            });
        });

    });

    function convertToJSON(value) {
        value = value.replace(/'/g, '!@#').replace(/"/g, "'").replace(/!@#/g, '"');
        return JSON.parse(value);
    }

</script>
)rawliteral";



void setup(void) { 
  
  Serial.begin(9600);

  temperatureSensorManager.begin();  

  // Display
  // by default, we'll generate the high voltage from the 3.3v line internally! (neat!)
  display.begin(SSD1306_SWITCHCAPVCC, 0x3C);  // initialize with the I2C addr 0x3D (for the 128x64)
  // init done

  // Show image buffer on the display hardware.
  // Since the buffer is intialized with an Adafruit splashscreen
  // internally, this will display the splashscreen.
  display.display();
  delay(2000);

  // Clear the buffer.
  display.clearDisplay();
  
  EEPROM.begin(MEM_ALOC_SIZE); 
  
  WiFi.config(ip, gateway, subnet);
  
  WiFi.mode(WIFI_AP_STA);
  WiFi.begin(ssid, password);

  // text display tests
  display.clearDisplay();
  display.setTextSize(1);
  display.setTextColor(WHITE);
  display.setCursor(0,0);
    
  display.println("Conectando Wifi...");      
  display.display();
  
  if(WiFi.waitForConnectResult() == WL_CONNECTED){

    display.println("Wifi conectado !!!");   
    display.display();   
    delay(2000);
    
    MDNS.begin(host);
      
    server.on("/", HTTP_GET, [](){
      // send to client
      server.send(200, "text/html", INDEX_HTML);
      // print the results to the serial monitor:
      Serial.print("Get Method = ");
      Serial.print("/");
    });

    server.on("/getCurrent", HTTP_GET, [](){
      // read args      
      //String jsonResult = "{'chip':'" + sensor.chip + "','addr':" + sensor.addrJSON + ",'data':" + sensor.dataJSON + ",'crc':" + sensor.crc + ",'type_s':" + sensor.type_s + ",'celsius':" + sensor.celsius + ",'fahrenheit':" + sensor.fahrenheit + "}";                 
      // send to client
      //server.send(200, "text/html", jsonResult);
      // print the results to the serial monitor:
      display.print("Get Method = ");
      display.print("getCurrent");
      display.print(" | ");
      display.print("jsonResult = ");
      //display.println(jsonResult);
      display.display();
    });
    
    server.on("/setPowerLamp", HTTP_POST, [](){      
      // read args
      bool value = server.arg("value") == "true" ? true : false;
      // power on led light
      //loop2();      
      // send to client
      server.send(200, "text/plain", (Update.hasError())?"FAIL":"OK");            
      // print the results to the serial monitor:
      display.print("Post Method = ");
      display.print("lamp");
      display.print(" | ");
      display.print("value = ");
      display.println(value);
      display.display();
    });
    
    server.begin();
    MDNS.addService("http", "tcp", 80);
  
    display.printf("Ready! Open http://%s.local in your browser\n", host);
    display.display();
  } else {
    display.println("WiFi Failed");
    display.display();
    delay(2000);
  }
}

// function to print a device address
void printAddressSerial(byte deviceAddress[8])
{
  for (uint8_t i = 0; i < 8; i++)
  {
    // zero pad the address if necessary
    if (deviceAddress[i] < 16) Serial.print("0");
    Serial.print(deviceAddress[i], HEX);
  }
}

void printAddressDisplay(byte deviceAddress[8])
{
  for (uint8_t i = 0; i < 8; i++)
  {
    // zero pad the address if necessary
    if (deviceAddress[i] < 16) display.print("0");
    display.print(deviceAddress[i], HEX);
  }
}

void printDataSerial(TemperatureSensor temperatureSensor)
{
  Serial.print("Address: "); 
  printAddressSerial(temperatureSensor.deviceAddress); 
  Serial.print(" ValidFamily: "); 
  Serial.print(temperatureSensor.validFamily); 
  Serial.print(" Family: "); 
  Serial.print(temperatureSensor.family); 
  Serial.print(" Connected: "); 
  Serial.print(temperatureSensor.isConnected); 
  Serial.print(" Resolution: "); 
  Serial.print(temperatureSensor.resolution); 
  Serial.print(" Temp C: ");
  Serial.print(temperatureSensor.tempCelsius);
  Serial.print(" Temp F: ");
  Serial.print(temperatureSensor.tempFahrenheit);
  Serial.print(" HasAlarm: ");
  Serial.print(temperatureSensor.hasAlarm);
  Serial.print(" LowAlarmTemp: ");
  Serial.print(temperatureSensor.lowAlarmTemp);
  Serial.print(" HighAlarmTemp: ");
  Serial.print(temperatureSensor.highAlarmTemp);
  Serial.println();
}

void loop(void){    

   // text display tests
  display.clearDisplay();
  display.setTextSize(1);
  display.setTextColor(WHITE);
  display.setCursor(0,0);

  server.handleClient();

  display.print("Mode=");
  display.println(digitalRead(16));
  
  TemperatureSensor *arr = temperatureSensorManager.getSensors();  
  for(int i = 0; i < sizeof(arr)/sizeof(int); ++i){
    printDataSerial(arr[i]);

    display.print("Address=");
    printAddressDisplay(arr[i].deviceAddress);
    display.println();

    display.setTextSize(2);
    display.print(arr[i].tempCelsius);    
    display.println(" C ");
    display.print(arr[i].tempFahrenheit);    
    display.println(" F");
  }  
  
  display.display();  
} 


