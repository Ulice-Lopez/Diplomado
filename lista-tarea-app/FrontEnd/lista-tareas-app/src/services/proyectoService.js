import api from "./api";

const endpoint = "/Proyectos";

// Obtener todos los proyectos
export const getProyectos = async () => {
  const response = await api.get(endpoint);
  return response.data;
};

// Crear proyecto
export const crearProyecto = async (proyecto) => {
  const response = await api.post(endpoint, proyecto);
  return response.data;
};

// Actualizar proyecto
export const actualizarProyecto = async (id, proyecto) => {
  const response = await api.put(endpoint, proyecto, {
    params: { id },
  });

  return response.data;
};

// Eliminar proyecto (borrado lógico)
// Cambia EstaActivo = false en la API
export const eliminarProyecto = async (id) => {
  const response = await api.delete(endpoint, {
    params: { id },
  });

  return response.data;
};
