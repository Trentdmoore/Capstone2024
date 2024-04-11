<template>

    <!-- APP BAR CODE -->
    <div>
        <v-app-bar :elevation="2" :color="'blue-darken-4'">
            <template v-slot:prepend>
                <v-app-bar-nav-icon></v-app-bar-nav-icon>
                <v-app-bar-title class="mx-auto">Application Bar</v-app-bar-title>
            </template>

            <v-spacer></v-spacer>

            <v-btn @click="GetTestingInfo()">Test API Call</v-btn>

            
        </v-app-bar>
    </div>

    <v-container>
        <v-row>
            <v-col cols="12">
                <v-data-table
                    :headers = "TableHeaders">
                
                    <template v-slot:top>
                        <v-toolbar
                            :color="'blue-lighten-1'"
                            :rounded="true"
                            dark
                        >
                            <v-toolbar-title>{{ currentTableView }}</v-toolbar-title>

                            <v-spacer></v-spacer>

                            <v-btn class="mx-auto">Access Logs</v-btn>

                            <v-btn class="mx-auto">Request Logs</v-btn>
                        </v-toolbar>
                    </template>
                </v-data-table>
            </v-col>

            <v-col cols="6">
                <v-form>
                    <v-row>
                        <v-col cols = 6>
                            <v-text-field variant="underlined" label="ID">
    
                            </v-text-field>
                        </v-col>
                            <v-col cols = 6>
                            <v-text-field variant="underlined" label="PID">
    
                            </v-text-field>
                            </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols = 6>
                            <v-text-field variant="underlined" label="First Name">
    
                            </v-text-field>
                        </v-col>
                            <v-col cols = 6>
                            <v-text-field variant="underlined" label="Last Name">
    
                            </v-text-field>
                            </v-col>
                    </v-row>
    
                    <v-row>
                        <v-col>
    
                        </v-col>
                    </v-row>
                </v-form>
            </v-col>
        </v-row>
    </v-container>


</template>

<script>
    import {personApi} from '../service/person.api.js'

    export default {
        data() {
            return {
                TableHeaders: [
                    {title: "ID", key: "Id"},
                    {title: "First Name", key: "FirstName"},
                    {title: "Last Name", key: "LastName"},
                ],
                currentTableView: "Person",
                selection: [],
                selectedRow: {},
                personObj: {},
                requestLogObj: {},
                accessLogObj: {},
                accessLogList: [],
                requestLogList: [],
                personList: [],

                
            }
        },
        methods: {
            GetTestingInfo() {
                personApi.getTestingInfo();
            }
        },
    }
</script>

<style scoped>

</style>