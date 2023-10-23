<script setup lang="ts">
import { ref } from "vue";
import { TestSignalRHub } from "../api/API_SIGNALR"; 
import * as signalr from '@microsoft/signalr';

let hubConnection = new signalr.HubConnectionBuilder()
                            .withUrl("https://localhost:8729/ws")
                            .build();

let client = new TestSignalRHub(hubConnection);

</script>

<template>

</template>