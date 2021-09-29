<template>
  <nav class="navbar navbar-expand-lg bg-secondary p-2">
    <div class="container-fluid">
      <router-link class="navbar-brand text-white" to="/">Dashboard</router-link>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link class="nav-link active text-white" to="/Publish">Publish</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link active text-white" to="/Trading">Trading</router-link>
          </li>

          <li class="nav-item">
            <router-link class="nav-link active text-white" to="/Portfolio">Portfolio</router-link>
          </li>
          <li class="nav-item">
            <a class="nav-link active text-white" href="#"></a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  stockName: "Navigation",
}
</script>

<style scoped>

</style>