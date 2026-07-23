<template>
  <div>
    <div class="d-flex justify-content-between align-items-center mb-4">
      <div>
        <h1 class="mb-1">Lista de proyectos</h1>
        <p>Administración de proyectos</p>
      </div>

      <button class="btn btn-primary" @click="nuevoProyecto">
        Nuevo proyecto
      </button>
    </div>

    <ProyectoForm
      v-if="mostrarFormulario"
      :proyectoEditar="proyectoSeleccionado"
      @guardar="guardarProyecto"
      @cancelar="cerrarFormulario"
    />

    <div class="card">
      <div class="card-body">
        <table class="table table-hover align-middle">
          <thead class="table-light">
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Activo</th>
              <th class="text-center">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-for="proyecto in proyectos" :key="proyecto.idProyecto">
              <td>
                {{ proyecto.idProyecto }}
              </td>

              <td>
                {{ proyecto.nombre }}
              </td>

              <td>
                <span
                  class="badge"
                  :class="proyecto.estaActivo ? 'bg-success' : 'bg-danger'"
                >
                  {{ proyecto.estaActivo ? "Sí" : "No" }}
                </span>
              </td>

              <td class="text-center">
                <button
                  class="btn btn-warning btn-sm me-2"
                  @click="editarProyecto(proyecto)"
                >
                  <i class="bi bi-pencil"></i>
                  Modificar
                </button>

                <button
                  class="btn btn-danger btn-sm"
                  @click="eliminarProyectoClick(proyecto.idProyecto)"
                >
                  <i class="bi bi-trash"></i>
                  Eliminar
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";

import ProyectoForm from "../components/proyectos/ProyectoForm.vue";

import {
  getProyectos,
  crearProyecto,
  actualizarProyecto,
  eliminarProyecto,
} from "../services/proyectoService";

const proyectos = ref([]);

const mostrarFormulario = ref(false);

const proyectoSeleccionado = ref(null);

const loading = ref(false);

const error = ref("");

// Abrir formulario nuevo

const nuevoProyecto = () => {
  proyectoSeleccionado.value = {
    idProyecto: 0,
    nombre: "",
  };

  mostrarFormulario.value = true;
};

// Abrir formulario editar

const editarProyecto = (proyecto) => {
  proyectoSeleccionado.value = {
    ...proyecto,
  };

  mostrarFormulario.value = true;
};

// Cerrar formulario

const cerrarFormulario = () => {
  mostrarFormulario.value = false;

  proyectoSeleccionado.value = null;
};

// Guardar / Actualizar

const guardarProyecto = async (proyecto) => {
  try {
    if (proyecto.idProyecto > 0) {
      await actualizarProyecto(proyecto.idProyecto, {
        nombre: proyecto.nombre,
      });
    } else {
      await crearProyecto({
        nombre: proyecto.nombre,
      });
    }

    await cargarDatos();

    cerrarFormulario();
  } catch (error) {
    console.error("Error al guardar proyecto:", error);
  }
};

// Eliminar proyecto

const eliminarProyectoClick = async (id) => {
  if (!confirm("¿Desea eliminar este proyecto?")) return;

  try {
    await eliminarProyecto(id);

    await cargarDatos();
  } catch (error) {
    console.error("Error al eliminar proyecto:", error);
  }
};

// Cargar proyectos

const cargarDatos = async () => {
  loading.value = true;

  error.value = "";

  try {
    proyectos.value = await getProyectos();
  } catch (err) {
    error.value = "No se pudieron cargar los proyectos.";

    console.error(err);
  } finally {
    loading.value = false;
  }
};

// Inicio

onMounted(() => {
  cargarDatos();
});
</script>
