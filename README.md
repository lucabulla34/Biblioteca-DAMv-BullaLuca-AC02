# Biblioteca-DAMv-BullaLuca-AC02

## Problemas con Jenkins
He tenido problemas al ejecutar la tarea en Jenkins unas tres o cuatro veces (Las otras 14-15 forman parte de la automatización hecha para que se ejecute cada cinco minutos). Los problemas eran:
· El PATH del dotnet no estaba correctamente configurado. Creí que sería una buena idea, entonces, aplicarlo directamente en el workflow.

![Jenkins Comandos Windows PowerShell](https://imgur.com/kdjFudv)

· El proyecto apuntado por los comandos por defecto no existía, puesto que inicialmente creamos un proyecto en SonarQube llamado "biblioteca-damv", pero los comandos por defecto apuntan a la Project Key "biblioteca-dam" (Matiz importante; lo corrigieron después). Bastaba con modificar el `k:/biblioteca-dam` --> `k:/biblioteca-damv`.

