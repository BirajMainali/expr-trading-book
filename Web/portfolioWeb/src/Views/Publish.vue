<script setup>
import {notify} from "@kyvg/vue3-notification";
import {onMounted, ref} from 'vue'
import axios from "axios";


const getDate = () => {
  let today = new Date();
  const dd = String(today.getDate()).padStart(2, '0');
  const mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
  const yyyy = today.getFullYear();
  today = mm + '/' + dd + '/' + yyyy;
  return today;
}

const viewModel = ref({
  stockName: '',
  quantity: 0,
  prefix: '',
  openingRate: 0,
  closingRate: 0,
  transactionDate: getDate(),
});


let stocks = ref({
  isLoading: true,
  data: []
});

const LoadInitial = () => {
  const data = viewModel.value;
  data.stockName = '';
  data.quantity = 0;
  data.prefix = '';
  data.openingRate = 0;
}

const getAddedStocks = async () => {
  try {
    stocks.value.data = (await axios.get("/api/Stock/index")).data
    stocks.value.isLoading = false
  } catch (e) {
    notify({
      title: e.message
    });
  }
}
const savePublish = async () => {
  try {
    if (!confirm("are you sure to publish")) return;
    const res = await axios.post("/api/Stock/New", {
      StockName: viewModel.value.stockName,
      prefix: viewModel.value.prefix,
      Quantity: viewModel.value.quantity,
      OpeningAmount: viewModel.value.openingRate
    });
    if (res.status === 200 || res.status === 201) {
      stocks.value.data.push({...res.data});
      LoadInitial();
      notify({
        title: "Ready for transaction"
      })
    }
  } catch (e) {
      console.log(e);
    notify({
      title: e.message
    });
  }
}
const Remove = async (id) => {
  try {
    if (!confirm("are you sure to remove this stock")) return;
    const res = await axios.delete(`/api/Stock/Remove/${id}`);
    if (res.status === 200 || res.status === 204) {
      stocks.value.data = stocks.value.data.filter(x => x.id !== id);
      notify({
        title: "Removed"
      })
    }
  } catch (e) {
    console.log(e);
    notify({
      title: e.message
    });
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
            <input type="text" id="Company" v-model="viewModel.stockName" class="form-control"
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
            <input type="number" id="OpeningAmount" v-model="viewModel.openingRate" class="form-control"
                   placeholder="400" required>
          </div>
        </div>
        <div class="col">
          <div class="btn-group">
            <button type="submit" class="btn btn-warning btn-sm mt-4" @click.prevent="savePublish"><i
                class="fa fa-upload"></i> Save
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
          <td>Opening Rate</td>
          <td>Closing Rate</td>
          <td>Action</td>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(stock,idx) in stocks.data" :key="idx">
          <td> {{ idx + 1 }}</td>
          <td>{{ stock.stockName }}</td>
          <td>{{ stock.prefix }}</td>
          <td>{{ stock.transactionDate }}</td>
          <td>{{ stock.quantity }}</td>
          <td>{{ stock.openingRate }}</td>
          <td>{{ stock.closingRate }}</td>
          <td>
            <button class="btn btn-sm btn-danger  " @click.prevent="Remove(stock.id)"><i class="fa fa-trash"></i></button>
          </td>
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
