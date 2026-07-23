<template>
  <div class="card shadow-sm mt-4">
    <div class="card-header bg-primary text-white">
      <h5 class="mb-0">
        {{ modoEdicion ? "Modificar Empleado" : "Nuevo Empleado" }}
      </h5>
    </div>

    <div class="card-body">
      <form @submit.prevent="guardar">
        <div class="mb-3">
          <label class="form-label">Nombre</label>
          <input
            type="text"
            class="form-control"
            v-model="empleado.nombre"
            required
          />
        </div>

        <div class="mb-3">
          <label class="form-label">Teléfono</label>
          <input
            type="text"
            class="form-control"
            v-model="empleado.telefono"
            required
          />
        </div>

        <div class="mb-3">
          <label class="form-label">Correo</label>
          <input
            type="email"
            class="form-control"
            v-model="empleado.correo"
            required
          />
        </div>

        <div class="form-check mb-3">
          <input
            class="form-check-input"
            type="checkbox"
            id="activo"
            v-model="empleado.estaActivo"
          />

          <label class="form-check-label" for="activo"> Empleado activo </label>
        </div>

        <div class="text-end">
          <button
            type="button"
            class="btn btn-secondary me-2"
            @click="cancelar"
          >
            Cancelar
          </button>

          <button type="submit" class="btn btn-primary">
            {{ modoEdicion ? "Actualizar" : "Guardar" }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, watch } from "vue";
import { actualizarEmpleado } from "../../services/EmpleadoService";

const props = defineProps({
  empleadoEditar: {
    type: Object,
    default: null,
  },
});

const emit = defineEmits(["guardar", "cancelar"]);

const empleado = ref({
  idEmpleado: props.empleadoEditar?.idEmpleado ?? 0,
  nombre: props.empleadoEditar?.nombre ?? "",
  telefono: props.empleadoEditar?.telefono ?? "",
  correo: props.empleadoEditar?.correo ?? "",
  estaActivo: props.empleadoEditar?.estaActivo ?? true,
});

watch(
  () => props.empleadoEditar,
  (nuevoEmpleado) => {
    if (nuevoEmpleado) {
      empleado.value = {
        idEmpleado: nuevoEmpleado.idEmpleado ?? 0,
        nombre: nuevoEmpleado.nombre ?? "",
        telefono: nuevoEmpleado.telefono ?? "",
        correo: nuevoEmpleado.correo ?? "",
        estaActivo: nuevoEmpleado.estaActivo ?? true,
      };
    }
  },
  { immediate: true },
);

const modoEdicion = computed(() => empleado.value.idEmpleado > 0);

const guardar = () => {
  emit("guardar", empleado.value);
};

const cancelar = () => {
  emit("cancelar");
};
</script>
