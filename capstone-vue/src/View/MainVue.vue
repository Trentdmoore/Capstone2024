<template>

    <!-- APP BAR CODE -->
    <v-app-bar :elevation="2" :color="'blue-darken-4'">
        <template v-slot:prepend>
            <v-app-bar-nav-icon></v-app-bar-nav-icon>
            <v-app-bar-title class="mx-auto">Application Bar</v-app-bar-title>
        </template>

        <v-spacer></v-spacer>
        
    </v-app-bar>

    <v-container>
        <v-row>
            <v-col cols="12">
                <v-data-table
                    :headers = "CurrentTableHeaders"
                    :items = "personList"
                    v-model="selectedRow"
                    return-object
                    select-strategy="single"
                    item-value="id"
                    :show-select="showSelected">
                
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

            <v-col cols="12">
                <v-form>
                    <v-row>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="ID" v-model="personObj.id">
    
                            </v-text-field>
                        </v-col>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="CID" v-model="personObj.cid">

                            </v-text-field>
                        </v-col>
                        
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Email" v-model="personObj.email">
    
                            </v-text-field>
                        </v-col>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Title" v-model="personObj.title">
    
                            </v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="First Name" v-model="personObj.fName">
    
                            </v-text-field>
                        </v-col>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Last Name" v-model="personObj.lName">
    
                            </v-text-field>
                        </v-col>

                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Access" v-model="personObj.accessCode">
    
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
                //Table Headers
                CurrentTableHeaders: [],
                PersonTableHeaders: [
                    {title: "ID", key: "id"},
                    {title: "First Name", key: "fName"},
                    {title: "Last Name", key: "lName"},
                    {title: "Email", key: "email"},
                    {title: "Title", key: "title"},
                    {title: "CID", key: "cid"},
                    {title: "Access", key: "accessCode"},
                ],
                RequestLogTableHeaders: [
                    {title: "ID", key: "id"},
                    {title: "Lock", key: "lockNum"},
                    {title: "Access", key: "accessCode"},
                    {title: "Date Requested", key: "dateRequested"},
                    {title: "Date Approved", key: "dateApproved"},
                    {title: "Advisor ID", key: "advisorID"},
                ],
                AccessLogTableHeaders: [
                    {title: "ID", key: "id"},
                    {title: "Lock", key: "lockNum"},
                    {title: "Time Accessed", key: "accessTime"},
                    {title: "CID", key: "cid"},
                    {title: "Accepted", key: "accepted"},
                ],
                currentTableView: "Person",
                selection: [],
                selectedRow: [],

                //DataBase Model Objects
                personObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: ""
                },
                requestLogObj: {},
                accessLogObj: {},

                //DataBase Return Lists
                accessLogList: [],
                requestLogList: [],
                personList: [],

                showSelected: true,

                
            }
        },
        methods: {
            async GetAllPersons() {
                await personApi.getAllPersons().then(response => {
                    this.personList = response;
                    this.CurrentTableHeaders = this.PersonTableHeaders;
                });
            },
        },
        watch: {
            selectedRow(){
                if(this.currentTableView === 'Person'){
                    if(this.selectedRow.length < 1){
                        this.personObj = {
                            id: "",
                            fName: "",
                            lName: "",
                            email: "",
                            title: "",
                            cid: "",
                            accessCode: ""
                        }
                    }
                    else{
                        this.personObj = this.selectedRow[0]
                    }
                }
            }
        },
        async beforeMount() {
            await this.GetAllPersons()
        }
        
    }
</script>

<style scoped>

</style>