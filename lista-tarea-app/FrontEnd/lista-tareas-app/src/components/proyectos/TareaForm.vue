<template>
  <div class="card mt-4">
    <div class="card-header">
      <h4>{{ form.idTarea > 0 ? "Editar Tarea" : "Nueva Tarea" }}</h4>
    </div>

    <div class="card-body">
      <form @submit.prevent="guardar">
        <!-- Nombre -->
        <div class="mb-3">
          <label class="form-label">Nombre</label>
          <input
            v-model="form.nombre"
            type="text"
            class="form-control"
            required
          />
        </div>

        <!-- Descripción -->
        <div class="mb-3">
          <label class="form-label">Descripción</label>
          <textarea
            v-model="form.descripcion"
            class="form-control"
            rows="3"
          ></textarea>
        </div>

        <!-- Fecha -->
        <div class="mb-3">
          <label class="form-label">Fecha</label>
          <input
            v-model="form.fecha"
            type="datetime-local"
            class="form-control"
            required
          />
        </div>

        <!-- Fecha Compromiso -->
        <div class="mb-3">
          <label class="form-label">Fecha Compromiso</label>
          <input
            v-model="form.fechaCompromiso"
            type="datetime-local"
            class="form-control"
            required
          />
        </div>

        <!-- Estado -->
        <div class="mb-3">
          <label class="form-label">Estado</label>

          <select v-model="form.estadoTarea" class="form-select">
            <option :value="1">Pendiente</option>
            <option :value="2">En proceso</option>
            <option :value="3">Terminada</option>
          </select>
        </div>

        <!-- Empleado -->
        <div class="mb-3">
          <label class="form-label">Empleado</label>

          <select v-model="form.idEmpleado" class="form-select">
            <option disabled value="">Seleccione un empleado</option>

            <option
              v-for="empleado in empleados"
              :key="empleado.idEmpleado"
              :value="empleado.idEmpleado"
            >
              {{ empleado.nombre }}
            </option>
          </select>
        </div>

        <!-- Proyecto -->
        <div class="mb-3">
          <label class="form-label">Proyecto</label>

          <select v-model="form.idProyecto" class="form-select">
            <option disabled value="">Seleccione un proyecto</option>

            <option
              v-for="proyecto in proyectos"
              :key="proyecto.idProyecto"
              :value="proyecto.idProyecto"
            >
              {{ proyecto.nombre }}
            </option>
          </select>
        </div>

        <!-- Activo -->
        <div class="form-check mb-3">
          <input
            class="form-check-input"
            type="checkbox"
            v-model="form.estaActivo"
          />

          <label class="form-check-label"> Activo </label>
        </div>

        <button type="submit" class="btn btn-success me-2">Guardar</button>

        <button
          type="button"
          class="btn btn-secondary"
          @click="$emit('cancelar')"
        >
          Cancelar
        </button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";

const props = defineProps({
  tareaEditar: Object,
  empleados: {
    type: Array,
    default: () => [],
  },
  proyectos: {
    type: Array,
    default: () => [],
  },
});

const emit = defineEmits(["guardar", "cancelar"]);

const form = ref({});

watch(
  () => props.tareaEditar,
  (nuevo) => {
    if (nuevo) {
      form.value = {
        ...nuevo,

        fecha: nuevo.fecha ? nuevo.fecha.substring(0, 16) : "",

        fechaCompromiso: nuevo.fechaCompromiso
          ? nuevo.fechaCompromiso.substring(0, 16)
          : "",
      };
    }
  },
  { immediate: true },
);

const guardar = () => {
  emit("guardar", {
    ...form.value,

    fecha: new Date(form.value.fecha).toISOString(),

    fechaCompromiso: new Date(form.value.fechaCompromiso).toISOString(),
  });
};
</script>
