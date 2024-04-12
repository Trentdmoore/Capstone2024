<template>
    <v-alert v-model="successAlert" type="success" :text="alertText" closable>
    </v-alert>
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
                    :row-props="colorSelectedRow"
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

                            <v-btn v-show="currentTableView==='Person'" class="mx-auto" @click="toggleCreatePersonDialog()">Create Person</v-btn>
                        </v-toolbar>
                    </template>
                </v-data-table>
            </v-col>

            <v-col cols="12">
                <v-form>
                    <v-row>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="ID" v-model="personSelectedObj.id">
    
                            </v-text-field>
                        </v-col>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="CID" v-model="personSelectedObj.cid">

                            </v-text-field>
                        </v-col>
                        
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Email" v-model="personSelectedObj.email">
    
                            </v-text-field>
                        </v-col>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Title" v-model="personSelectedObj.title">
    
                            </v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="First Name" v-model="personSelectedObj.fName">
    
                            </v-text-field>
                        </v-col>
                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Last Name" v-model="personSelectedObj.lName">
    
                            </v-text-field>
                        </v-col>

                        <v-col cols = 3>
                            <v-text-field variant="underlined" label="Access" v-model="personSelectedObj.accessCode">
    
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

    <v-dialog v-model="showCreatePersonDialog">
        <v-card width="1000" height="400" class="mx-auto">
            <v-card-title>Create Person</v-card-title>
            <v-card-subtitle>Fill out form and click sumbit</v-card-subtitle>
                      
            <v-card-item>
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
                <v-btn :color="'success'" class="mr-3" @click="InsertPersonInfo()">Submit</v-btn>
                <v-btn :color="'error'" @click="toggleCreatePersonDialog()">Close</v-btn>
            </v-card-item>
        </v-card>

    </v-dialog>

    

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
                personSelectedObj: {
                    id: "",
                    fName: "",
                    lName: "",
                    email: "",
                    title: "",
                    cid: "",
                    accessCode: ""
                },
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

                showCreatePersonDialog: false,

                successAlert: false,
                alertText: "",

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
            async InsertPersonInfo(){
                await personApi.insertPersonInfo(this.personObj).then(response => {
                    if(response === 'Success'){
                        this.alertText = `${this.personObj.fName} ${this.personObj.lName} was successfully created`
                        this.successAlert = true;
                        this.toggleCreatePersonDialog();
                        this.LoadTable();
                    }
                    else{
                        this.alertText = `${this.personObj.fName} ${this.personObj.lName} failed to created`
                    }
                })
            },
            LoadTable(){
                this.GetAllPersons();
            },
            colorSelectedRow(row) {
                if(row.item === this.selectedRow[0]){
                    return {style: 'background: #42A5F5; color: white;'};
                }
            },
            toggleCreatePersonDialog(){
                if(this.showCreatePersonDialog === false){
                    this.showCreatePersonDialog = true;
                }
                else{
                    this.showCreatePersonDialog = false;
                }
            }
        },
        watch: {
            selectedRow(){
                if(this.currentTableView === 'Person'){
                    if(this.selectedRow.length < 1){
                        this.personSelectedObj = {
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
                        this.personSelectedObj = this.selectedRow[0]
                    }
                }
            }
        },
        async beforeMount() {
            this.LoadTable();
        },
        created(){
            setTimeout(() =>{
                this.successAlert=false
            }, 3000)
        }
        
    }
</script>

<style scoped>

</style>