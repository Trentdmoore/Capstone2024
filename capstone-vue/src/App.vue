<template>
  <v-app>
    <v-main>
      <MainVue></MainVue>
    </v-main>
  </v-app>
</template>

<script>
import MainVue from './View/MainVue.vue'

export default {
  name: 'App',

  components: {
    MainVue
  },

  data: () => ({
    //
  }),
}
</script>
