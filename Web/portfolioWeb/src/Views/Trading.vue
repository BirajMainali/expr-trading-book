<template>
  <div class="card">
    <div class="card-header">
      <h5 class="card-title"><i class="fa fa-chart-line"></i> BUY/SELL</h5>
    </div>
    <div class="card-body">
      <div class="row">
        <div class="col-4">
          <div class="form-group">
            <label>Stock</label>
            <select type="number" class="form-control" v-model="tradingViewModel.stockId">
              <option v-for="stock in tradingViewModel.StockData" :key="stock.id" :value="stock.id">
                [ {{ stock.prefix }} ] {{ stock.name }}
              </option>
            </select>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <label for="quantity">Quantity</label>
            <input type="number" id="quantity" min="10" required v-model="tradingViewModel.Quantity"
                   class="form-control">
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <label>Price</label>
            <input type="text" v-model="tradingViewModel.Price" class="form-control">
          </div>
        </div>
        <div class="col">
          <div class="from-group">
            <label>Transaction Type</label>
            <select type="number" class="form-control" v-model="tradingViewModel.TransactionType">
              <option value="1">BUY</option>
              <option value="2">SELL</option>
            </select>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <label>Transaction Date</label>
            <input type="date" v-model="tradingViewModel.TransactionDate" class="form-control">
          </div>
        </div>
        <div class="col">
          <div class="btn-group">
            <button class="btn mt-4 btn-danger" @click.prevent="orderTransaction"> Proceed</button>
          </div>
        </div>
      </div>
    </div>
    <div class="card-footer">
      <table class="table table-bordered table-striped">
        <thead>
        <tr>
          <td>#</td>
          <td>Company</td>
          <td>Transaction</td>
          <td>Date</td>
          <td>Quantity</td>
          <td>Price</td>
          <td>Investment</td>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(stock,idx) in tradingViewModel.History" :key="idx">
          <td>{{ idx + 1 }}</td>
          <td>{{ stock.stockName }}</td>
          <td>{{ stock.transactionType }}</td>
          <td>{{ stock.transactionDate }}</td>
          <td>{{ stock.quantity }}</td>
          <td>{{ stock.price }}</td>
          <td>{{ stock.quantity * stock.price }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import {onMounted, ref, watch} from "vue";
import axios from "axios";

const tradingViewModel = ref({
  stockId: 0,
  Quantity: 0,
  TransactionType: null,
  Price: 0,
  TransactionDate: '',
  StockData: [],
  History: [],
})


const orderTransaction = async () => {
  try {
    if (!confirm("are you sure to Proceed")) return;
    if (!tradingViewModel.value.Quantity || !tradingViewModel.value.TransactionType || !tradingViewModel.value.TransactionDate) return;
    await axios.post(`/api/StockTransaction/AddTransaction`, {
      stockId: tradingViewModel.value.stockId,
      quantity: tradingViewModel.value.Quantity,
      transactionType: tradingViewModel.value.TransactionType,
      price: tradingViewModel.value.Price,
      transactionDate: tradingViewModel.value.TransactionDate
    });
  } catch (e) {
    console.log(e.message)
  }
}

const LoadTradingStock = async () => {
  tradingViewModel.value.StockData = (await axios.get("/api/Stock/Stocks")).data.data;
}

watch(() => tradingViewModel.value.stockId, () => {
  setTimeout(async () => {
    tradingViewModel.value.History = (await axios.get(`/api/StockTransaction/History/${tradingViewModel.value.stockId}`)).data.data;
  }, 0);
})


onMounted(async () => {
  await LoadTradingStock();
})

</script>

<style scoped>

</style>