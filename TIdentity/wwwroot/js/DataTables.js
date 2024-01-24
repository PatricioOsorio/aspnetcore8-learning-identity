
//se implemento la traduccion a español del datatable en forma parcial ya que se uso una version anterior
//la version implementada se encontraba traducida un 96%
//actualmente el idioma español se encuentra traducido al 100% y lo puedes consultar en 
//el siguiente enlace: https://datatables.net/plug-ins/i18n/Spanish_Mexico.html
$(document).ready(function () {
  $('#MyTable').DataTable({
    dom: 'Bfrtip',
    buttons: {
      buttons: [
        {
          extend: 'copy',
          titleAttr: 'Copiar',
          className: 'btn',
          text: '<i class="fa-solid fa-copy"></i>',
          orientation: 'landscape',
        },
        {
          extend: 'csv',
          titleAttr: 'Descargar CSV',
          className: 'btn',
          text: '<i class="fas fa-file-csv"></i>',
          orientation: 'landscape',
        },
        {
          extend: 'pdf',
          titleAttr: 'Descargar PDF',
          className: 'btn',
          text: '<i class="fas fa-file-pdf"></i>',
          orientation: 'landscape'
        },
        {
          extend: 'excel',
          titleAttr: 'Descargar Excel',
          className: 'btn',
          text: '<i class="fas fa-file-excel"></i>',
          orientation: 'landscape',
        },
        {
          extend: 'print',
          titleAttr: 'Imprimir',
          className: 'btn',
          text: '<i class="fa-solid fa-print"></i>',
          orientation: 'landscape',
        },

      ],
      dom: {
        button: {
          className: 'btn btn-secondary',
        },
      },
    },
    language: {
      "aria": {
        "sortAscending": "Activar para ordenar la columna de manera ascendente",
        "sortDescending": "Activar para ordenar la columna de manera descendente"
      },
      "autoFill": {
        "cancel": "Cancelar",
        "fill": "Rellene todas las celdas con <i>%d&lt;\\\/i&gt;<\/i>",
        "fillHorizontal": "Rellenar celdas horizontalmente",
        "fillVertical": "Rellenar celdas verticalmente"
      },
      "buttons": {
        "collection": "Colección",
        "colvis": "Visibilidad",
        "colvisRestore": "Restaurar visibilidad",
        "copy": "Copiar",
        "copyKeys": "Presione ctrl o u2318 + C para copiar los datos de la tabla al portapapeles del sistema. <br \/> <br \/> Para cancelar, haga clic en este mensaje o presione escape.",
        "copySuccess": {
          "1": "Copiada 1 fila al portapapeles",
          "_": "Copiadas %d fila al portapapeles"
        },
        "copyTitle": "Copiar al portapapeles",
        "csv": "CSV",
        "excel": "Excel",
        "pageLength": {
          "-1": "Mostrar todas las filas",
          "1": "Mostrar 1 fila",
          "_": "Mostrar %d filas"
        },
        "pdf": "PDF",
        "print": "Imprimir"
      },
      "decimal": ",",
      "emptyTable": "No se encontraron resultados",
      "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
      "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
      "infoFiltered": "(filtrado de un total de _MAX_ registros)",
      "infoThousands": ",",
      "lengthMenu": "Mostrar _MENU_ registros",
      "loadingRecords": "Cargando...",
      "paginate": {
        "first": "Primero",
        "last": "Último",
        "next": "Siguiente",
        "previous": "Anterior"
      },
      "processing": "Procesando...",
      "search": "Buscar:",
      "searchBuilder": {
        "add": "Añadir condición",
        "button": {
          "0": "Constructor de búsqueda",
          "_": "Constructor de búsqueda (%d)"
        },
        "clearAll": "Borrar todo",
        "condition": "Condición",
        "data": "Data",
        "deleteTitle": "Eliminar regla de filtrado",
        "leftTitle": "Criterios anulados",
        "logicAnd": "Y",
        "logicOr": "O",
        "rightTitle": "Criterios de sangría",
        "title": {
          "0": "Constructor de búsqueda",
          "_": "Constructor de búsqueda (%d)"
        },
        "value": "Valor",
        "conditions": {
          "date": {
            "after": "Después",
            "before": "Antes",
            "between": "Entre",
            "empty": "Vacío",
            "equals": "Igual a",
            "not": "Diferente de",
            "notBetween": "No entre",
            "notEmpty": "No vacío"
          },
          "number": {
            "between": "Entre",
            "empty": "Vacío",
            "equals": "Igual a",
            "gt": "Mayor a",
            "gte": "Mayor o igual a",
            "lt": "Menor que",
            "lte": "Menor o igual a",
            "not": "Diferente de",
            "notBetween": "No entre",
            "notEmpty": "No vacío"
          },
          "string": {
            "contains": "Contiene",
            "empty": "Vacío",
            "endsWith": "Termina con",
            "equals": "Igual a",
            "not": "Diferente de",
            "notEmpty": "Nop vacío",
            "startsWith": "Inicia con"
          },
          "array": {
            "equals": "Igual a",
            "empty": "Vacío",
            "contains": "Contiene",
            "not": "Diferente",
            "notEmpty": "No vacío",
            "without": "Sin"
          }
        }
      },
      "searchPanes": {
        "clearMessage": "Borrar todo",
        "collapse": {
          "0": "Paneles de búsqueda",
          "_": "Paneles de búsqueda (%d)"
        },
        "count": "{total}",
        "emptyPanes": "Sin paneles de búsqueda",
        "loadMessage": "Cargando paneles de búsqueda",
        "title": "Filtros Activos - %d",
        "countFiltered": "{shown} ({total})"
      },
      "select": {
        "1": "%d fila seleccionada",
        "_": "%d filas seleccionadas",
        "cells": {
          "1": "1 celda seleccionada",
          "_": "$d celdas seleccionadas"
        },
        "columns": {
          "1": "1 columna seleccionada",
          "_": "%d columnas seleccionadas"
        }
      },
      "thousands": ",",
      "zeroRecords": "No se encontraron resultados",
      "datetime": {
        "previous": "Anterior",
        "hours": "Horas",
        "minutes": "Minutos",
        "seconds": "Segundos",
        "unknown": "-",
        "amPm": [
          "am",
          "pm"
        ],
        "next": "Siguiente"
      },
      "editor": {
        "close": "Cerrar",
        "create": {
          "button": "Nuevo",
          "title": "Crear Nuevo Registro",
          "submit": "Crear"
        },
        "edit": {
          "button": "Editar",
          "title": "Editar Registro",
          "submit": "Actualizar"
        },
        "remove": {
          "button": "Eliminar",
          "title": "Eliminar Registro",
          "submit": "Eliminar",
          "confirm": {
            "_": "¿Está seguro que desea eliminar %d filas?",
            "1": "¿Está seguro que desea eliminar 1 fila?"
          }
        },
        "error": {
          "system": "Ha ocurrido un error en el sistema (<a target=\"\\\" rel=\"\\ nofollow\" href=\"\\\">Más información&lt;\\\\\\\/a&gt;).&lt;\\\/a&gt;<\/a>"
        },
        "multi": {
          "title": "Múltiples Valores",
          "info": "Los elementos seleccionados contienen diferentes valores para este registro. Para editar y establecer todos los elementos de este registro con el mismo valor, hacer click o tap aquí, de lo contrario conservarán sus valores individuales.",
          "restore": "Deshacer Cambios",
          "noMulti": "Este registro puede ser editado individualmente, pero no como parte de un grupo."
        }
      }
    }
  });
});