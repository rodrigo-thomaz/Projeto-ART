/*
  CeilingFanLightStation - Library for Ceiling Fan Light Station code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/
 
#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <EEPROM.h>

#include <LedLight.h>
#include <CeilingFan.h>
#include <Settings.h>

#define MEM_ALOC_SIZE 4

const char* host = "CeilingFanLightStation";
const char* ssid = "RThomaz";
const char* password = "2919517400";

IPAddress ip(192, 168, 1, 177);
IPAddress gateway(192,168,1,1); 
IPAddress subnet(255,255,255,0); 

ESP8266WebServer server(80);

LedLight ledLight(
      5   // Digital output pin that the LED is attached to 
  );

CeilingFan ceilingFan(
      4   // Digital output pin that the Fan Direction Forward is attached to
    , 13  // Digital output pin that the Fan Direction Reverse is attached to
    , 16  // Analog output pin that the Fan Speed is attached to
    , 0   // Address of last speed of fan direction forward
    , 1   // Address of default value of fan direction reverse
  );  

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
        <fieldset>
            <legend>Modo</legend>
            <input type='radio' name='optFanDirection' value='forward'> Ventilador<br>
            <input type='radio' name='optFanDirection' value='none'> Desligado<br>
            <input type='radio' name='optFanDirection' value='reverse'> Exaustor
        </fieldset>
        <fieldset>
            <legend>Velocidade</legend>
            <input type='range' id="rngFanSpeed" min='0' max='255' />
        </fieldset>
    </form>
    <div class="loader"></div>
</body>
</html>

<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>

<script type='text/javascript'>

    var url = 'http://192.168.0.49/';

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

        $('input[type=radio][name=optFanDirection]').change(function () {
            var fanDirection = this.value;
            var data = 'value=' + fanDirection;
            $.ajax({
                url: url + 'setFanDirection',
                type: 'post',
                data: data,
                success: function (response) {
                    response = convertToJSON(response);
                    $('#rngFanSpeed').val(response.fanSpeed);
                    $("#rngFanSpeed").prop('disabled', fanDirection == 'none');
                },
                error: function (xhr) {
                    //Do Something to handle error
                }
            });
        });

        $('#rngFanSpeed').on('change', function () {
            var fanDirection = $('input[type=radio][name=optFanDirection]:checked').val();
            var data = 'fanDirection=' + fanDirection + '&value=' + this.value;
            $.ajax({
                url: url + 'setFanSpeed',
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

static const char PROGMEM SETTINGS_HTML[] = R"rawliteral(
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
            <legend>Field 1</legend>
            
        </fieldset>
        <fieldset>
            <legend>Field 2</legend>
            
        </fieldset>
        <fieldset>
            <legend>Field 3</legend>
            
        </fieldset>
    </form>
    <div class="loader"></div>
</body>
</html>

<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>

<script type='text/javascript'>

    var url = 'http://192.168.0.25/';

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

        
    });

    function convertToJSON(value) {
        value = value.replace(/'/g, '!@#').replace(/"/g, "'").replace(/!@#/g, '"');
        return JSON.parse(value);
    }

</script>
)rawliteral";

void setup(void){
  
  Serial.begin(9600);

  EEPROM.begin(MEM_ALOC_SIZE);
  
  Serial.println();
  Serial.println("Booting Sketch...");      
  
  WiFi.config(ip, gateway, subnet);
  
  WiFi.mode(WIFI_AP_STA);
  WiFi.begin(ssid, password);
  
  if(WiFi.waitForConnectResult() == WL_CONNECTED){
    
    MDNS.begin(host);
      
    server.on("/", HTTP_GET, [](){
      // send to client
      server.send(200, "text/html", INDEX_HTML);
      // print the results to the serial monitor:
      Serial.print("Get Method = ");
      Serial.print("/");
    });

    server.on("/settings", HTTP_GET, [](){
      // send to client
      server.send(200, "text/html", SETTINGS_HTML);
      // print the results to the serial monitor:
      Serial.print("Get Method = ");
      Serial.print("/settings");
    });

    server.on("/getCurrent", HTTP_GET, [](){
      // read args
      String lamp = ledLight.getPower() ? "true" : "false";
      String fanDirection = ceilingFan.getFanDirection();     
      String fanSpeed = String(ceilingFan.getFanSpeed());      
      String jsonResult = "{'lamp':" + lamp + ",'fanDirection':'" + fanDirection + "','fanSpeed':" + fanSpeed + "}";     
      // send to client
      server.send(200, "text/html", jsonResult);
      // print the results to the serial monitor:
      Serial.print("Get Method = ");
      Serial.print("getCurrent");
      Serial.print(" | ");
      Serial.print("jsonResult = ");
      Serial.println(jsonResult);
    });
    
    server.on("/setPowerLamp", HTTP_POST, [](){      
      // read args
      bool value = server.arg("value") == "true" ? true : false;
      // power on led light
      ledLight.setPower(value);      
      // send to client
      server.send(200, "text/plain", (Update.hasError())?"FAIL":"OK");            
      // print the results to the serial monitor:
      Serial.print("Post Method = ");
      Serial.print("lamp");
      Serial.print(" | ");
      Serial.print("value = ");
      Serial.println(value);
    });
    
    server.on("/setFanDirection", HTTP_POST, [](){      
      // read args
      String value = server.arg("value");    
      // set fan direction
      byte fanSpeed = ceilingFan.setFanDirection(value);
      // send to client      
      String jsonResult = "{'fanSpeed':'" + String(fanSpeed) + "'}";
      server.send(200, "text/plain", jsonResult);      
      // print the results to the serial monitor:
      Serial.print("Post Method = ");
      Serial.print("fanDirection");
      Serial.print(" | ");
      Serial.print("value = ");
      Serial.print(value);
      Serial.print(" | ");
      Serial.print("fanSpeed = ");
      Serial.println(fanSpeed);
    });

    server.on("/setFanSpeed", HTTP_POST, [](){            
      // read args
      String fanDirection = server.arg("fanDirection");
      int value = server.arg("value").toInt();
      // set fan speed
      ceilingFan.setFanSpeed(fanDirection, value);
      // send to client
      server.send(200, "text/plain", (Update.hasError())?"FAIL":"OK");
      // print the results to the serial monitor:
      Serial.print("Post Method = ");
      Serial.print("speed");
      Serial.print(" | ");
      Serial.print("fanDirection = ");
      Serial.print(fanDirection);  
      Serial.print(" | ");
      Serial.print("value = ");
      Serial.println(value);  
    });
    
    server.begin();
    MDNS.addService("http", "tcp", 80);
  
    Serial.printf("Ready! Open http://%s.local in your browser\n", host);
  } else {
    Serial.println("WiFi Failed");
  }
}
 
void loop(void){
  server.handleClient();
  delay(1);
} 
