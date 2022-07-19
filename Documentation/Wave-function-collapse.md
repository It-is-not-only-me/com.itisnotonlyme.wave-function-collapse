# Documentacion
---

Esta implementacion es suficientemente flexible, pero esto viene con una cierta configuracion para el caso especifico. La forma para hacer esto es implementando las siguientes interfaces: 
 * IEstado
 * IValor

### IEstado
---
Esta interface va a representar los posibles estados en los que puede estar cada nodo. Y tienen la responsabilidad de determinar si son posibles en relacion a otros estados determinados. 

Como ejemplo: en un sudoku, tendriamos un estado que representaria los numeros. Y si esta determinado un numero, estas tiene que desaparecer como posibilidades en el nodo.

### IValor
---
Esta interface permite representar la relacion que tienen los nodos por las aristas.

Como ejemplo: en una matriz, tendriamos un valor para representar la direccion (arriba, abajo, izquierda, derecha) en la que se encuentra los nodos.

### ISeleccionarNodo e ISeleccionarEstado
---
Son interfaces para poder elegir, ya sea nodos o estados, segun una lista de los mismo. Esto permite poder elegir ya sea conociendo los nodos o estados, y tener que cada elemeneto tenga su propio peso.