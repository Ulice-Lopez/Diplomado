import api from "./api";

const endpoint = "/Empleados";

// Obtener todos
export const getEmpleados = async () => {
  const response = await api.get(endpoint);
  return response.data;
};

// Crear
export const crearEmpleado = async (empleado) => {
  const response = await api.post(endpoint, empleado);

  return response.data;
};

// Actualizar
export const actualizarEmpleado = async (id, empleado) => {
  const response = await api.put(endpoint, empleado, {
    params: {
      id: id,
    },
  });

  return response.data;
};

// Eliminar
export const eliminarEmpleado = async (id) => {
  const response = await api.delete(endpoint, {
    params: {
      id: id,
    },
  });

  return response.data;
};
