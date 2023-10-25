<script setup lang="ts">
import { ref } from "vue";
import { TestMessage, TestSignalRHub } from "../api/API_SIGNALR"; 
import * as signalr from '@microsoft/signalr';

const message = ref("");
const testMessageResp = ref("");

let hubConnection = new signalr.HubConnectionBuilder()
                            .withUrl("https://localhost:8729/ws")
                            .build();

hubConnection.on("UserJoined", (id: string) => {
    console.log("UserJoined1: " + id);
});

hubConnection.on("UserJoined", (id: string) => {
    console.log("UserJoined2: " + id);
});

let client = new TestSignalRHub(hubConnection);

async function testMessage() {
    testMessageResp.value = (await client.testMessage(new TestMessage({ message: message.value }))).message;
}

hubConnection.start();

</script>

<template>
    <form class="row" @submit.prevent="testMessage">
        <input id="test-message-input" v-model="message" placeholder="Enter a name..." />
        <button type="submit">Message Response Test</button>
    </form>

    <p>{{ testMessageResp }}</p>
</template>