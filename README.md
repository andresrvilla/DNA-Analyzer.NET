

![DNA- Analyzer](http://www.andres-villa.com.ar/dna-analyzer.png)
API REST para detectar secuencias de ADN

## Objetivo

Este proyecto tiene como objetivo evaluar conocimientos de Roberto A. Villa por parte del equipo de recruiting de MercadoLibre.

## Introducción

Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar contra los X-Men. Con dicho objetivo se ha desarrollado una API REST para detectar secuencias de ADN dentro de un ADN dado. Dicha API recibe como parámetro un array de Strings que representan cada fila de una tabla de (NxN) con la secuencia del ADN. Las letras de los Strings solo pueden ser: (A,T,C,G), las
cuales representa cada base nitrogenada del ADN.

#### Ejemplos de ADN

![Ejemplos ADN](http://www.andres-villa.com.ar/dna-examples.png)

Un humano es mutante si encuentra más de una secuencia de cuatro letras iguales​, de forma oblicua, horizontal o vertical.

## Status del Proyecto

Nivel 3 completado: 
* API REST con el objetivo de detectar ADN mutante deployada en Azure. 
* Agregado de estadísticas y persistencia en Base de Datos. 
* Test Coverage: >80%

## Instrucciones para su prueba

El servicio se encuentra deployado en https://dnaanalyzer.azurewebsites.net/](https://dnaanalyzer.azurewebsites.net/)

El servicio actualmente cuenta con los siguientes métodos:
 
 * Método POST para detectar si un ADN dado es mutante:<br><br>
  La URL del método es [https://dnaanalyzer.azurewebsites.net/mutant](https://dnaanalyzer.azurewebsites.net/mutant)<br><br>
Se puede detectar si un humano es mutante enviando la secuencia de ADN mediante un HTTP POST con un Json el cual tenga el siguiente formato:<br><br>
POST → /mutant/<br />
{<br />
"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]<br />
}<br><br>
En caso de verificar que el ADN enviado es mutante, el método devuelve como respuesta un HTTP 200-OK, en caso contrario un
403-Forbidden <br><br>
* Método GET para obtener las estadísticas de las verificaciones de ADN para Magneto<br><br>
La URL del método es [https://dnaanalyzer.azurewebsites.net/stats](https://dnaanalyzer.azurewebsites.net/stats)<br><br>

Se puede probar utilizando [Postman](https://www.getpostman.com/). Puede importar el proyecto desde [El siguiente vínculo](https://www.getpostman.com/collections/717c334070e97e8dbbf9)

## Descarga del código fuente
   
  
   #### Repositorio
   
   El código se encuentra alojado en github. Para descargarlo necesita un cliente git, que puede encontrarlo en https://git-scm.com/downloads
   
   * Cree una carpeta en donde se incluirá el código fuente<br>
   * Abra su consola y posicionese en la carpeta previamente creada<br>
   * Ejecute el comando<br>
   
    git clone https://github.com/robertoavilla/DNA-Analyzer.NET.git
   
   Luego de que termine la descarga, usted tendrá clonado el branch master en la carpeta previamente creada.

## Environment

* [.NET Framework 4.6.1](https://www.microsoft.com/en-us/download/details.aspx?id=49981)
* IDE: [Visual Studio Community Edition](https://visualstudio.microsoft.com/)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express)
* EntityFramework
* Unity IOC Container
* Newtonsoft.Json
* Deployment en Amazon (Free Tier)
* Moq
* StyleCop

## Análisis Inicial

Analizando más alla del requerimiento funcional, podemos observar que lo que se está solicitando construir es un Analizado de ADN. Luego, ese Analizador será utilizado para el objetivo específico de detectar si el ADN corresponde con un Mutante, respetando la lógica funcional dada.

En un mundo con mutantes, según un análisis de proyeccion a futuro, es posible imaginar que los siguientes escenarios se den:

* Se requiera detectar un tipo particular de mutantes, por ejemplo que tenga 2 o 3 de las posibles cadenas que definen a un mutante. Por ejemplo, podriamos tener que "Mutantes que disparan" son los que tienen en su adn más de una secuencia de 4 bases nitrogenadas, restringidas a las bases A y T
* Se descubra que una persona tenga "rasgos mutantes" cuando tiene una secuencia de 4 bases nitrogenadas y luego al menos una secuencia de 3 bases nitrogenadas
* Se descubra que la secuencia de determinada combinacion de secuencias determine que un mutante es "malo" por lo tanto candidato a ser reclutado
* Se descubra que una persona es mutante determinada por nuevas combinaciones de bases nitrogenadas.

De esta manera, podemos preveer que existe la posibilidad de que se añadan nuevos requerimientos.

Además, podríamos imaginar que se analice el ADN con otros objetivos, por ejemplo médicos

Por lo tanto para preveer posibles nuevos requerimientos o nuevos clientes, el proyecto ha sido pensado como un "Analizador de ADN" generico, con la implementación de API particular para Magneto.

Se ha dividido en:

* Core
	* Se trata de la funcionalidad básica de búsqueda de ADN: Dado un ADN, buscar en su interior

 	* Debe tenerse en cuenta que los siguientes resultados de búsquedas pueden ser posibles:<br>
 		* Obtener la cantidad<br>
 		* Obtener un booleano (No implementado)<br>
 		* Obtener lista de ocurrencias (No implementado)<br>

	* En particular, para cumplir con el requerimiento de Magneto, se define una búsqueda en todas las direcciones (Horizontal, Vertical y Diagonal) de una secuencia dada, dando como resultado la cantidad de ocurrencias de esa secuencia.

	* Existe la posibilidad de agregar nuevas formas de búsqueda.
	* Estructura de configuración de la busqueda:

	![DNA- Analyzer](http://www.andres-villa.com.ar/Structure.png)
	
Para realizar el analisis de ADN se debe utilizar el Analisis Set correspondiente 
* Particular
	* Se crean los controllers requeridos para responder con los requerimientos técnicos
	* Se crea una clase de servicio que opera con las clases de análisis respetando los requerimientos funcionales.
	* Se crea un JSON de configuración de Análisis de Mutante para ser utilizado en la API
	* Se crea una instancia del AnalysisSet correspondiente a la búsqueda de Mutante como un Singleton por medio de Unity
## Test Coverage

El detalle de coverage se puede ver en detalle en [https://dnaanalyzer.azurewebsites.net/CoverageDetail](https://dnaanalyzer.azurewebsites.net/CoverageDetail)

(Las pruebas de integración fueron excluidas del coverage para que el reporte sea exclusivamente de Tests Unitarios)

**Line coverage:**	82.9%
**Branch coverage:**	95.6%

## Known Issues / Mejoras

* Se ha utilizado StyleCop como soporte para mantener un estilo uniforme de código. Se ha deshabilitado completamente la solicitud de Documentación, salvo los headers del assembly DNAAnalyzer.NET.Bussiness. Se debe incrementar la cantidad de documentación requerida e ir completandola. El output de StyleCop al momento del deploy se puede encontrar en [https://dnaanalyzer.azurewebsites.net/StylecopOutput.txt](https://dnaanalyzer.azurewebsites.net/StylecopOutput.txt)
* StyleCop define reglas de estilo que son contradictorias con algunas sugerencias de Visual Studio. Se debe analizar si dichas reglas son necesarias. Las reglas son las siguientes:
	* Todo miembro de una clase debe ser referenciado siempre con this. (Visual Studio sugiere simplificarlo)
	* Todos los using de System deben incluirse al inicio de los using (Visual Studio sugiere ordenar de forma alfafetica todos los using, incluyendo a System)
* Se debe buscar la posibilidad de que configurar por medio de JSON sea de una forma mas clara. No obstante esta complejidad es solucionada abstrayendola en la clase de servicio.
* Agregar Logging de errores, previo analisis de impacto de performance.
* Analizar si es factible a nivel negocio cachear los datos de /stats para evitar consultas excesivas a la base de datos

## Atribuciones de terceros
* [Vector de Fondo creado por ikatod](https://www.freepik.es/vector-gratis/vector-de-red-de-fondo-triangulo-de-poligono-abstracto_1306336.htm)
* Icons made by [Freepik](http://www.freepik.com) from [Flaticon](https://www.flaticon.com/)