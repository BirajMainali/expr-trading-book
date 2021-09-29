<template>
  <div class="card">
    <div class="card-header">
      <div class="card-title">My Portfolio</div>
    </div>
    <div class="card-body">
      <table class="table table-striped table-bordered table-hover">
        <thead>
        <tr>
          <td>#</td>
          <td>Stock</td>
          <td>In</td>
          <td>Out</td>
          <td>Investment</td>
          <td>Sold</td>
          <td>Remaining Qty</td>
          <td>Current Valuation</td>
          <td>OverAll Profit/Loss</td>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(item,idx) in state.portfolio" :key="idx">
          <td>{{ idx + 1 }}</td>
          <td>{{ item.stock }}</td>
          <td>{{ item.totalSold }}</td>
          <td>{{ item.totalUnit }}</td>
          <td>{{ item.totalInvestment }}</td>
          <td>{{ item.soldAmount }}</td>
          <td>{{ item.remaining }}</td>
          <td>{{ item.currentAmount }}</td>
          <td>{{ item.overAllProfit }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import {onMounted, ref} from "vue";
import axios from "axios";

const state = ref({
  portfolio: [],
});

const getPortfolio = async () => {
  state.value.portfolio = (
      await axios.get("/api/StockTransaction/Portfolio")
  ).data;
};

onMounted(async () => {
  await getPortfolio();
});
</script>

<style scoped></style>
