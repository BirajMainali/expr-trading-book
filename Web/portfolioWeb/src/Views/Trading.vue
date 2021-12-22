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
              <option v-for="stock in state.StockData" :key="stock.id" :value="stock.id">
                [ {{ stock.prefix }} ] {{ stock.stockName }}
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
            <input type="number" v-model="tradingViewModel.Price" class="form-control">
          </div>
        </div>
        <div class="col">
          <div class="from-group">
            <label>Transaction Type</label>
            <select class="form-control" v-model.number="tradingViewModel.TransactionType">
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
            <button class="btn mt-4 btn-warning brn-sm" @click.prevent="orderedTransaction"><i class="fa fa-check-circle"></i> Add</button>
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
          <td>Action</td>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(stock,idx) in state.History" :key="idx">
          <td>{{ idx + 1 }}</td>
          <td>{{ stock.stock }}</td>
          <td>{{ stock.transactionType }}</td>
          <td>{{ stock.transactionDate }}</td>
          <td>{{ stock.quantity }}</td>
          <td>{{ stock.price }}</td>
          <td>{{ stock.quantity * stock.price }}</td>
           <td>
            <button class="btn btn-sm btn-danger  " @click.prevent="Remove(stock.id)"><i class="fa fa-trash"></i></button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import {onMounted, ref, watch} from "vue";
import axios from "axios";
import {notify} from "@kyvg/vue3-notification";

const tradingViewModel = ref({
  Id: 0,
  stockId: 0,
  Quantity: 0,
  TransactionType: '',
  Price: 0,
  TransactionDate: '',
})
const state = ref({
  StockData: [],
  History: [],
})

const loadInitial = () => {
  const temp = tradingViewModel.value;
  temp.stockId = 0;
  temp.Quantity = 0;
  temp.TransactionDate = '';
  temp.Price = 0;
  temp.TransactionDate = '';
}

const orderedTransaction = async () => {
  try {
    if (!confirm("are you sure to Proceed")) return;
    if (!tradingViewModel.value.Quantity || !tradingViewModel.value.TransactionType || !tradingViewModel.value.TransactionDate) return;
    const res = await axios.post(`/api/StockTransaction/New`, {
      stockId: tradingViewModel.value.stockId,
      quantity: tradingViewModel.value.Quantity,
      transactionType: tradingViewModel.value.TransactionType,
      price: tradingViewModel.value.Price,
      transactionDate: tradingViewModel.value.TransactionDate
    });
    if (res.status === 200 || res.status === 201) {
      state.value.History.push({...res.data});
      loadInitial();
      notify({
        title: "Transaction Completed",
      });
    }
  } catch (e) {
    notify({
      title: e
    });
  }
}

const Remove = async (id) => {
  try {
    if (!confirm("are you sure to remove this transaction")) return;
    const res = await axios.delete(`/api/StockTransaction/Remove/${id}`);
    if (res.status === 200 || res.status === 204) {
      state.value.History =state.value.History.filter(x => x.id !== id);
      notify({
        title: "Removed"
      })
    }
  } catch (e) {
    notify({
      title: e.message
    });
  }
}

const LoadTradingStock = async () => {
  state.value.StockData = (await axios.get("/api/Stock/Index")).data;
}

watch(() => tradingViewModel.value.stockId, () => {
  const id = tradingViewModel.value.stockId;
  setTimeout(async () => {
    if (id !== 0 || id) {
      state.value.History = (await axios.get(`/api/StockTransaction/History/?id=${id}`)).data;
    }
  }, 0);
})


onMounted(async () => {
  await LoadTradingStock();
})

</script>

<style scoped>

</style>