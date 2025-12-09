# Biblioteca-DAMv-BullaLuca-AC02

## Problemas con Jenkins
He tenido problemas al ejecutar la tarea en Jenkins unas tres o cuatro veces (Las otras 14-15 forman parte de la automatización hecha para que se ejecute cada cinco minutos). Los problemas eran:

· El PATH del dotnet no estaba correctamente configurado. Creí que sería una buena idea, entonces, aplicarlo directamente en el workflow.

![Jenkins Comandos Windows PowerShell](https://imgur.com/kdjFudv.jpg)

· El proyecto apuntado por los comandos por defecto no existía, puesto que inicialmente creamos un proyecto en SonarQube llamado "biblioteca-damv", pero los comandos por defecto apuntan a la Project Key "biblioteca-dam" (Matiz importante; lo corrigieron después). Bastaba con modificar el `k:/biblioteca-dam` --> `k:/biblioteca-damv`.

## Preguntes de Reflexió
#### Pregunta 1: Identificació de problemes
Després d'executar SonarQube, observa l'informe generat:
Quins són els 3 problemes més crítics detectats? 
· Un dels problemes més importants és el Code Coverage. No es fa cap unit test, i per tant el coverage és del 0%.
![Jenkins Comandos Windows PowerShell](https://i.imgur.com/zpVzBQX.png)

· També n'hi ha onze problemes de manteniment de categoria mitjana. Molts d'aquests es redueixen a escalabilitat del codi, ja que són poc eficaços o no tenen cobertes posibles excepcions com ara valors nuls.

Per què creus que SonarQube els classifica així?
Quin impacte podrien tenir aquests problemes en producció?
· Crec que SonarQube classifica els problemes en funció del temps que es trigaria en arreglar cada problema i la seva gravetat. Per exemple, si després d'enviar el codi a producció hi sorgeix un bug crític, un codi amb una mantenibilitat baixa serà difícil i llarg d'arreglar, mentre que un codi llegible i amb una mantenibilitat alta serà més fàcil.

Pregunta 2: Correcció de codi
Tria un dels problemes detectats i proposa una solució:

· En el meu cas triaré un `Console.ReadLine()`.

Com modificaries el codi per solucionar-lo?

· Depenent del cas, modificaria el codi per suprimir l'advertència de valor nul, sigui amb un `!` o amb un `string?` o amb un `string a = Console.ReadLine() ?? "(text per si s'envia un null)";`.

Quines bones pràctiques apliques?

· En el meu cas crec que podria fins i tot millorar-ho fent servir una constant, ja que el cas del ReadLine() és un freqüent, i crec que seria factible fer servir cosntants.

Com pots verificar que la solució és correcta?

· Pujant el codi al GitHub un altre cop i, quan el workflow s'actualitzi (cada 5 minuts), veurà el canvi reflexat i em donarà una nova valoració.

Pregunta 3: Integració contínua
Quina diferència veus entre executar SonarQube manualment i integrar-lo en un pipeline CI/CD?

· Crec que pot estalviar temps i facilitar la feina, ja que si hem de fer-ho manualment, considero que seria fer una vegada enrere l'altra les mateixes comandes, però crec que aquest és un procés que es pot simplificar moltíssim.

Quins avantatges aporta l'automatització?

· Apart de la reutilització, aporta estandarització. Uns tests unitaris automàtics sempre seràn fiables.

En quin moment del desenvolupament és més útil executar aquestes anàlisis?

· Abans d'enviar-ho a producció, és quan s'ha de fer el Review del codi i aturar-lo si fos necessari.



Pregunta 4: Comparació d'eines
Has treballat amb SonarQube, Jenkins i GitHub Actions:
Quina eina t'ha semblat més fàcil de configurar? Per què?
Quins avantatges i desavantatges veus en cadascuna?
Quina utilitzaries en un projecte real i per què?

· Crec que GitHub Actions és la més senzilla, com que el programari està allotjat a GitHub, només fa falta crear el workflow i el ci.yml, la resta es converteix en un procés tan senzill com només fer commits i pushes, el CI farà la resta. En quant a avantatges i desavantatges, veig que SonarQube fa una "in-depth" review, molt llegible i comprensible, pots llegir còmodament quins són els problemes i corregir-los. Crec que és la més profesional i l'eina que més utilitats pot tenir. Jenkins, d'una altra banda, no és tan senzilla de configurar com GitHub o SonarQube, però et permet fer servir més paràmetres als tests. 
· En un projecte real faria servir SonarQube i/o GitHub Actions, però principalment SonarQube. Considero que la importància d'un anàlisi en detall és el fet diferencial, i SonarQube compleix amb nota en aquest apartat.

Pregunta 5: Mètriques de qualitat
A l'informe de SonarQube, observa les mètriques:
Què significa la "complexitat ciclomàtica"?
Per què és important la cobertura de tests?
Quin percentatge de deute tècnic (technical debt) considera acceptable?

· La complexitat ciclomàtica és la quantitat de camins independents que pot prendre un codi. Per cada cas possible, s'hi suma +1 a la complexitat. Un codi amb baixa complexitat és un codi més llegible i més "testeable". La cobertura de tests és **vital**, ja que sense una alta cobertura, el codi tindrà escenaris amb possibilitats de bugs. Crec que un deute tècnic acceptable ha de tenir una cobertura de més del 80% del codi, i estar sense bugs ni vulnerabilitats.

Pregunta 6: Workflow en equip
Imagina que treballes en un equip de 5 desenvolupadors:
Com utilitzaries aquestes eines per assegurar la qualitat del codi?
Quin workflow proposaries (branching, PR, CI/CD)?
Què faries si algú fa un commit que falla l'anàlisi?

· Faria, a GitHub, projectes amb user stories, issues i sub-issues en funció de les funcionalitats del codi. Un cop finalitzades cada issue de cada US, implementaria un test unitari automatitzat per cada cas, assegurant la qualitat del codi. El fluxe de treball el separaria per funcionalitat. Cada funcionalitat tindrà una branca, i cada branca tindrà el seu CI per provar les funcionalitats. Aquest CI ha de reaccionar al moment de fer una PR, i s'ha d'aprovar en el moment en què el CI no dona cap error i el codi té una puntuació acceptable a SonarQube. Si algú fa un commit que falla l'anàlisi, aquesta part del codi s'ha de modificar per tal d'aconseguir que el codi funcioni bé.



# MSTest 

Primerament hem de crear la llibreria del programa a testejar, on ficarem el Program.cs (Hem d'anomenar-ho com a (nom)Utils.cs pel codi, i (nom)TestUtils.cs pel test.

A la llibreria, creem una classe. Implementem i reanomenem.

```c#
using System.Linq.Expressions;

namespace WarriorClass
{
    public class WarriorUtils
    {
        public static void CalculateWarriorStatus(int hp, out string status,
            out double speedPercentage, out double attackPercentage, out bool canRun, out string screenColour)
        {
            hp = Math.Min(hp, 100);

            if (hp == 0)
            {
                status = "Dead";
                speedPercentage = 0;
                attackPercentage = 0;
                canRun = false;
                screenColour = "red";
            }
            else if (hp >= 1 && hp <= 25)
            {
                status = "Critical";
                speedPercentage = 50;
                attackPercentage = 50;
                canRun = false;
                screenColour = "normal";
            }
            else if (hp >= 26 && hp <= 50)
            {
                status = "Severely Injured";
                speedPercentage = 70;
                attackPercentage = 80;
                canRun = false;
                screenColour = "normal";
            }
            else if (hp >= 51 && hp <= 75)
            {
                status = "Injured";
                speedPercentage = 90;
                attackPercentage = 100;
                canRun = true;
                screenColour = "normal";
            }
            else
            {
                status = "Healthy";
                speedPercentage = 100;
                attackPercentage = 100;
                canRun = true;
                screenColour = "normal";
            }
        }
    }
}


```

Després d'implementar el codi, creem el test. Hem d'anar a l'Explorador d'Arxius > Afegir projecte > MSTest (C#)
<img width="648" height="542" alt="image" src="https://github.com/user-attachments/assets/bcefcbec-aaff-41be-b078-b95bd9685815" />


Un cop creat el Test, hem de fer l'AAA (Arrange > Act > Assert). Primerament hem de referenciar el projecte. Explorador d'Arxius > Afegir > Referenciar Projecte > Seleccionem la classe a referenciar. A l'arxiu .csproj, s'ens afegirà una nova línea.
<img width="1333" height="588" alt="image" src="https://github.com/user-attachments/assets/0772bba0-954b-4118-97ca-bb51e8fbec82" />

```c#
using TestWarriorMethods;
using WarriorClass;

namespace TestWarriorMethods
{
    [TestClass]
    public sealed class TestWarriorUtils
    {
        [TestMethod]
        public void TC1_VerifyStatus_With_MaxHP()
        {
            // Arrange
            double speed, attack;
            bool canRun;
            string status, screenColour;
            // Act
            WarriorUtils.CalculateWarriorStatus(100, out status, out speed, out attack, out canRun, out screenColour);

            // Assert
            Assert.AreEqual("Healthy", status);
            Assert.AreEqual(100, speed);
            Assert.AreEqual(100, attack);
            Assert.IsTrue(canRun);
            Assert.AreEqual("normal", screenColour);
        }
    }
}
```
