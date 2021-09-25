<script setup>
import {onMounted, ref} from 'vue'
import axios from "axios";

const viewModel = ref({
  id: Math.random(),
  name: '',
  quantity: 0,
  prefix: '',
  openingAmount: 0,
  date: Date.now().toFixed()
});

let stocks = ref({
  isLoading: true,
  data: []
});

const LoadInitial = () => {
  const data = viewModel.value;
  data.name = '';
  data.quantity = 0;
  data.prefix = '';
  data.openingAmount = 0;
}

const getAddedStocks = async () => {
  try {
    stocks.value.data = (await axios.get("/api/Stock/Stocks")).data.data;
    stocks.value.isLoading = false
  } catch (e) {
    console.warn(e);
  }
}
const savePublish = async () => {
  try {
    if (!confirm("are you sure to publish")) return;
    const res = await axios.post("/api/Stock/New", {
      StockName: viewModel.value.name,
      prefix: viewModel.value.prefix,
      Quantity: viewModel.value.quantity,
      OpeningAmount: viewModel.value.openingAmount
    });
    stocks.value.data.push({...viewModel.value});
    LoadInitial();
    console.warn(res.data.notify)
  } catch (e) {

    console.warn(e);
  }
}
const Remove = async (id) => {
  try {
    if (!confirm("are you sure to remove this stock")) return;
    const res = await axios.post(`/api/Stock/Remove/${id}`);
    if (res.status === 200) {
      stocks.value.data = stocks.value.data.filter(x => x.id !== id);
    }
  } catch (e) {
    console.warn(e);
  }
}

onMounted(async () => {
  await getAddedStocks();
})
</script>

<template>
  <div class="card">
    <div class="card-header">
      <h5 class="card-title"> Publish Stock</h5>
    </div>
    <div class="card-body">
      <div class="row">
        <div class="col-4">
          <div class="form-group">
            <label for="Company">Publisher Company</label>
            <input type="text" id="Company" v-model="viewModel.name" class="form-control"
                   placeholder="Union life insurance" required>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <label for="Quantity">Quantity</label>
            <input type="number" id="Quantity" v-model="viewModel.quantity" class="form-control" placeholder="1,00,00"
                   required>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <label for="Prefix">Prefix</label>
            <input type="text" id="Prefix" v-model="viewModel.prefix" class="form-control" placeholder="ULI" required>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <label for="OpeningAmount">Opening Amount</label>
            <input type="number" id="OpeningAmount" v-model="viewModel.openingAmount" class="form-control"
                   placeholder="400" required>
          </div>
        </div>
        <div class="col">
          <div class="btn-group">
            <button type="submit" class="btn btn-danger btn-sm mt-4" @click.prevent="savePublish"><i
                class="fa fa-upload"></i> Publish
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="card-footer">
      <h5><i class="fa fa-info-circle"></i> Current Issues</h5>
      <table class="table table-bordered table-striped">
        <thead>
        <tr>
          <td>#</td>
          <td>Name</td>
          <td>Prefix</td>
          <td>Publish Date</td>
          <td>Quantity</td>
          <td>Opening Amount</td>
          <td>Action</td>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(stock,idx) in stocks.data" :key="idx">
          <td> {{ idx + 1 }}</td>
          <td>{{ stock.name }}</td>
          <td>{{ stock.prefix }}</td>
          <td>{{ stock.date }}</td>
          <td>{{ stock.quantity }}</td>
          <td>{{ stock.openingAmount }}</td>
          <td>
            <button class="btn btn-sm btn-danger" @click.prevent="Remove(stock.id)"> Remove</button>
          </td>
          <td></td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>


<style scoped>
a {
  color: #42b983;
}
</style>
