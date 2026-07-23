<template>
  <div>
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h1>Empleados</h1>

      <button class="btn btn-primary" @click="nuevoEmpleado">
        Agregar Empleado
      </button>
    </div>

    <table class="table table-hover align-middle">
      <thead class="table-light">
        <tr>
          <th>ID</th>
          <th>Nombre</th>
          <th>Teléfono</th>
          <th>Correo</th>
          <th>Activo</th>
          <th class="text-center">Acciones</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="empleado in empleados" :key="empleado.idEmpleado">
          <td>
            {{ empleado.idEmpleado }}
          </td>

          <td>
            {{ empleado.nombre }}
          </td>

          <td>
            {{ empleado.telefono }}
          </td>

          <td>
            {{ empleado.correo }}
          </td>

          <td>
            <span
              class="badge"
              :class="empleado.estaActivo ? 'bg-success' : 'bg-danger'"
            >
              {{ empleado.estaActivo ? "Sí" : "No" }}
            </span>
          </td>

          <td class="text-center">
            <button
              class="btn btn-warning btn-sm me-2"
              @click="editarEmpleado(empleado)"
            >
              Modificar
            </button>

            <button
              class="btn btn-danger btn-sm"
              @click="eliminarEmpleado(empleado.idEmpleado)"
            >
              Eliminar
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- FORMULARIO -->

    <EmpleadoForm
      v-if="empleadoEditar"
      :empleadoEditar="empleadoEditar"
      @guardar="guardarEmpleado"
      @cancelar="cancelarEdicion"
    />
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";

import {
  getEmpleados,
  crearEmpleado,
  actualizarEmpleado,
  eliminarEmpleado as eliminarEmpleadoService,
} from "../services/empleadoService";

import EmpleadoForm from "../components/proyectos/EmpleadoForm.vue";

const empleados = ref([]);

const empleadoEditar = ref(null);

// Cargar empleados

const cargarEmpleados = async () => {
  try {
    empleados.value = await getEmpleados();
  } catch (error) {
    console.error("Error cargando empleados:", error);
  }
};

// Nuevo empleado

const nuevoEmpleado = () => {
  empleadoEditar.value = {
    idEmpleado: 0,

    nombre: "",

    telefono: "",

    correo: "",

    estaActivo: true,
  };
};

// Editar empleado

const editarEmpleado = (empleado) => {
  empleadoEditar.value = {
    ...empleado,
  };
};

// Guardar

const guardarEmpleado = async (empleado) => {
  try {
    const datos = {
      nombre: empleado.nombre,

      telefono: empleado.telefono,

      correo: empleado.correo,
    };

    if (empleado.idEmpleado > 0) {
      await actualizarEmpleado(empleado.idEmpleado, datos);
    } else {
      await crearEmpleado(datos);
    }

    await cargarEmpleados();

    empleadoEditar.value = null;
  } catch (error) {
    console.error("Error guardando empleado:", error);
  }
};

// Cancelar

const cancelarEdicion = () => {
  empleadoEditar.value = null;
};

// Eliminar

const eliminarEmpleado = async (id) => {
  if (!confirm("¿Desea eliminar este empleado?")) return;

  try {
    await eliminarEmpleadoService(id);

    await cargarEmpleados();
  } catch (error) {
    console.error("Error eliminando empleado:", error);
  }
};

onMounted(() => {
  cargarEmpleados();
});
</script>
