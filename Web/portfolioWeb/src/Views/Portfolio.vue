<template>
  <div class="card">
    <div class="card-header">
      <div class="card-title">
        My Portfolio
      </div>
    </div>
    <div class="card-body">
      <div class="row">
        <div class="col-4">
          <div class="form-group">
            <label>Scrip</label>
            <input type="text" class="form-control">
          </div>
        </div>
        <div class="col mt-4">
          <div class="input-group">
            <button class="btn btn-dark"><i class="fa fa-filter"></i></button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>

import {onMounted, ref} from "vue";
import axios from "axios";

const state = ref({
  portfolio: [],
})

const getPortfolio = async () => {
  state.value.portfolio = (await axios.get('/')).data;
}

onMounted(async () => {
  await getPortfolio();
})

</script>

<style scoped>

</style>