<template>
  <div>
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h1>Tareas</h1>

      <button class="btn btn-primary" @click="nuevaTarea">Agregar Tarea</button>
    </div>

    <table class="table table-hover align-middle">
      <thead class="table-light">
        <tr>
          <th>ID</th>
          <th>Nombre</th>
          <th>Descripción</th>
          <th>Fecha</th>
          <th>Compromiso</th>
          <th>Estado</th>
          <th>Empleado</th>
          <th>Proyecto</th>
          <th>Activo</th>
          <th class="text-center">Acciones</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="tarea in tareas" :key="tarea.idTarea">
          <td>{{ tarea.idTarea }}</td>
          <td>{{ tarea.nombre }}</td>
          <td>{{ tarea.descripcion }}</td>
          <td>{{ formatearFecha(tarea.fecha) }}</td>
          <td>{{ formatearFecha(tarea.fechaCompromiso) }}</td>
          <td>{{ mostrarEstado(tarea.estadoTarea) }}</td>
          <td>{{ obtenerNombreEmpleado(tarea.idEmpleado) }}</td>
          <td>{{ obtenerNombreProyecto(tarea.idProyecto) }}</td>

          <td>
            <span
              class="badge"
              :class="tarea.estaActivo ? 'bg-success' : 'bg-danger'"
            >
              {{ tarea.estaActivo ? "Sí" : "No" }}
            </span>
          </td>

          <td class="text-center">
            <button
              class="btn btn-warning btn-sm me-2"
              @click="editarTarea(tarea)"
            >
              Modificar
            </button>

            <button
              class="btn btn-danger btn-sm"
              @click="eliminarTarea(tarea.idTarea)"
            >
              Eliminar
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <TareaForm
      v-if="tareaEditar"
      :tareaEditar="tareaEditar"
      :empleados="empleados"
      :proyectos="proyectos"
      @guardar="guardarTarea"
      @cancelar="cancelarEdicion"
    />
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";

import { getEmpleados } from "../services/empleadoService";

import { getProyectos } from "../services/proyectoService";

import {
  getTareas,
  crearTarea,
  actualizarTarea,
  eliminarTarea as eliminarTareaService,
} from "../services/TareaService.js";

import TareaForm from "../components/proyectos/TareaForm.vue";

const empleados = ref([]);
const proyectos = ref([]);

const tareas = ref([]);
const tareaEditar = ref(null);

const cargarTareas = async () => {
  try {
    tareas.value = await getTareas();
  } catch (error) {
    console.error(error);
  }
};

const cargarCatalogos = async () => {
  try {
    empleados.value = await getEmpleados();
    proyectos.value = await getProyectos();

    console.log("Empleados:", empleados.value);
    console.log("Proyectos:", proyectos.value);
  } catch (error) {
    console.error(error);
  }
};

const obtenerNombreEmpleado = (idEmpleado) => {
  const empleado = empleados.value.find((e) => e.idEmpleado === idEmpleado);
  return empleado ? empleado.nombre : idEmpleado;
};

const obtenerNombreProyecto = (idProyecto) => {
  const proyecto = proyectos.value.find((p) => p.idProyecto === idProyecto);
  return proyecto ? proyecto.nombre : idProyecto;
};

const nuevaTarea = () => {
  tareaEditar.value = {
    idTarea: 0,
    nombre: "",
    descripcion: "",
    fecha: new Date().toISOString(),
    fechaCompromiso: new Date().toISOString(),
    idEmpleado: 0,
    idProyecto: 0,
  };
};

const editarTarea = (tarea) => {
  tareaEditar.value = { ...tarea };
};

const guardarTarea = async (tarea) => {
  try {
    if (tarea.idTarea > 0) {
      await actualizarTarea(tarea.idTarea, tarea);
    } else {
      await crearTarea({
        nombre: tarea.nombre,
        descripcion: tarea.descripcion,
        fecha: tarea.fecha,
        fechaCompromiso: tarea.fechaCompromiso,
        idEmpleado: tarea.idEmpleado,
        idProyecto: tarea.idProyecto,
      });
    }

    await cargarTareas();
    tareaEditar.value = null;
  } catch (error) {
    console.error("Error al guardar tarea:", error);
  }
};

const mostrarEstado = (estado) => {
  switch (estado) {
    case 1:
      return "Pendiente";
    case 2:
      return "En proceso";
    case 3:
      return "Terminada";
    default:
      return "Desconocido";
  }
};

const cancelarEdicion = () => {
  tareaEditar.value = null;
};

const eliminarTarea = async (id) => {
  try {
    await eliminarTareaService(id);
    await cargarTareas();
  } catch (error) {
    console.error(error);
  }
};

const formatearFecha = (fecha) => {
  return new Date(fecha).toLocaleDateString();
};

onMounted(() => {
  cargarCatalogos();
  cargarTareas();
});
</script>
