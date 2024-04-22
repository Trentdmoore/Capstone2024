<template>
    <!-- APP BAR CODE -->
    <v-app-bar
        :color="'red-darken-4'"
        prominent
      >
        <v-app-bar-nav-icon variant="text" @click.stop="showNavDrawer = !showNavDrawer"></v-app-bar-nav-icon>

        <v-toolbar-title :style="'text-align: left'">Home</v-toolbar-title>

        <v-spacer></v-spacer>

        <v-btn icon="mdi-magnify" variant="text"></v-btn>

        <v-btn icon="mdi-filter" variant="text"></v-btn>

        <v-btn icon="mdi-dots-vertical" variant="text"></v-btn>
      </v-app-bar>

      <v-navigation-drawer
        v-model="showNavDrawer"
        location="start"
        temporary
      >
        <v-list>
          <v-list-item
            v-for="option in navOptions"
            :key="option.value"
            :to="option.route">
            <v-list-item-content>{{ option.title }}</v-list-item-content>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
</template>

<script>
    export default {
        data() {
            return {
                showNavDrawer: false,
                navOptions: [
                    {
                        title: 'Home',
                        value: 'home',
                        route: '/'
                    },
                    {
                        title: 'Create Person',
                        value: 'createPerson',
                        route: '/Create'
                    },
                    {
                        title: 'Manage Employee',
                        value: 'managaeEmployee',
                        route: '/Manage'
                    },
                    {
                        title: 'Logs Menu',
                        value: 'logMenu',
                        route: '/Logs'
                    }
                ],
            }
        },
        methods: {

        }
    }
</script>

<style lang="scss" scoped>

</style>