<script setup lang="ts">
import { ref } from "vue";
import { Client, TestMessage } from '../api/API_REST';

const getRestResp = ref("");
const message = ref("");
const getPostResp = ref("");

let client = new Client("https://localhost:8729");

async function get() {
  getRestResp.value = (await client.restTestGET()).toJSON();
}

async function post() {
  getPostResp.value = (await client.restTestPOST(new TestMessage({ message: message.value }))).toJSON();
}

</script>

<template>
    <form class="row" @submit.prevent="get">
      <button type="submit">Rest Get Test</button>
    </form>
  
    <p>{{ getRestResp }}</p>

    
    <form class="row" @submit.prevent="post">
        <input id="post-input" v-model="message" placeholder="Enter a name..." />
        <button type="submit">Rest Post Test</button>
    </form>

    <p>{{ getPostResp }}</p>
</template>