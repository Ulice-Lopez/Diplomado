<template>
  <div class="sidebar p-3">
    <h5 class="text-white mb-4">Menú</h5>

    <div class="nav flex-column">
      <router-link class="nav-link text-white" to="/"> Home </router-link>

      <router-link class="nav-link text-white" to="/proyecto">
        Proyectos
      </router-link>

      <router-link class="nav-link text-white" to="/empleado">
        Empleados
      </router-link>

      <router-link class="nav-link text-white" to="/tarea">
        Tareas
      </router-link>

      <router-link class="nav-link text-white" to="/about"> About </router-link>
    </div>
  </div>
</template>

<style scoped>
.sidebar {
  width: 250px;
  min-height: 100vh;
  background: #212529;
}

.nav-link {
  border-radius: 8px;
  margin-bottom: 5px;
  transition: 0.3s;
}

.nav-link:hover {
  background: rgba(255, 255, 255, 0.1);
}

.router-link-active {
  background: #0d6efd;
}
</style>
