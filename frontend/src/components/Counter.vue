<template>
  <section>
    <h1>{{ title }}</h1>
    <span>{{ counter }}</span>
  
    <div class="grid grid-cols-2 gap-1 font-bold">
      <button @click="increase" class="p-3 bg-red-300 rounded-lg"> +1 </button>
      <button @click="decrease" class="p-3 bg-red-300 rounded-lg"> -1 </button>
    </div>
  </section>
</template>

<script lang="ts">
export default {
  data() {
    return {
      counter: 10,
      title: 'Counter'
    }
  },
  methods: {
    increase() {
      return this.counter++;
    },
    decrease() {
      return this.counter --;
    }
  }
}
</script>



<style scoped>
section {
  width: 300px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

h1 {
  font-size: 30px;
}

span {
  margin: 10px 0;
  font-size: 20px;
  font-weight: 600;
}
</style>