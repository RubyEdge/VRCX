<script setup lang="ts">
import { ref } from "vue";
import { TestMessage, TestSignalRHub } from "../api/API_SIGNALR"; 
import * as signalr from '@microsoft/signalr';

const message = ref("");
const testMessageResp = ref("");

let hubConnection = new signalr.HubConnectionBuilder()
                            .withUrl("wss://localhost:8729/ws")
                            .build();

let client = new TestSignalRHub(hubConnection);

async function testMessage() {
    testMessageResp.value = (await client.testMessage(new TestMessage({ message: message.value }))).toJSON();
}

</script>

<template>
    <form class="row" @submit.prevent="testMessage">
        <input id="post-input" v-model="message" placeholder="Enter a name..." />
        <button type="submit">Message Response Test</button>
    </form>

    <p>{{ testMessageResp }}</p>
</template>