import api from "./api";

const endpoint = "/Tareas";

export const getTareas = async () => {
  const response = await api.get(endpoint);
  return response.data;
};

export const crearTarea = async (tarea) => {
  const response = await api.post(endpoint, tarea);
  return response.data;
};

export const actualizarTarea = async (id, tarea) => {
  const response = await api.put(endpoint, tarea, {
    params: { id },
  });

  return response.data;
};

export const eliminarTarea = async (id) => {
  const response = await api.delete(endpoint, {
    params: {
      id: id,
    },
  });

  return response.data;
};
