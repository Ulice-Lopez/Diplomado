<template>
  <div class="card shadow">
    <div class="card-header bg-primary text-white">
      <h5 class="mb-0">
        {{ modoEdicion ? "Modificar Proyecto" : "Nuevo Proyecto" }}
      </h5>
    </div>

    <div class="card-body">
      <form @submit.prevent="guardar">
        <!-- Nombre -->
        <div class="mb-3">
          <label class="form-label"> Nombre del proyecto </label>

          <input
            type="text"
            class="form-control"
            v-model="proyecto.nombre"
            placeholder="Ingrese el nombre del proyecto"
            required
          />
        </div>

        <!-- Botones -->
        <div class="d-flex justify-content-end gap-2">
          <button type="button" class="btn btn-secondary" @click="cancelar">
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
import { ref, computed } from "vue";

const props = defineProps({
  proyectoEditar: {
    type: Object,
    default: null,
  },
});

const emit = defineEmits(["guardar", "cancelar"]);

const proyecto = ref({
  idProyecto: props.proyectoEditar?.idProyecto ?? 0,

  nombre: props.proyectoEditar?.nombre ?? "",
});

const modoEdicion = computed(() => {
  return proyecto.value.idProyecto > 0;
});

const guardar = () => {
  emit("guardar", proyecto.value);
};

const cancelar = () => {
  emit("cancelar");
};
</script>
