<template>
    <!--Logs View Form-->
    <v-container class="mt-16">
        <v-form>
            <v-row>
                <v-col>
                    <v-data-table
                    :style="'text-align: left'"
                    :headers = "CurrentTableHeaders1"
                    :items = "accessLogList"
                    :row-props="colorSelectedRow"
                    v-model="selectedRow"
                    return-object
                    select-strategy="single"
                    item-value="id"
                    :show-select="showSelected">
                
                    <template v-slot:top>
                        <v-toolbar
                            :color="'teal-lighten-1'"
                            :rounded="true"
                            dark
                        >
                            <v-toolbar-title :style="'text-align: left'">{{ accessLogTableView }}</v-toolbar-title>

                            <v-spacer></v-spacer>

                        </v-toolbar>
                    </template>
                    </v-data-table>
                </v-col>
                <v-col>
                    <v-data-table
                    :style="'text-align: left'"
                    :headers = "CurrentTableHeaders2"
                    :items = "requestLogList"
                    :row-props="colorSelectedRow"
                    v-model="selectedRow"
                    return-object
                    select-strategy="single"
                    item-value="id"
                    :show-select="showSelected">
                
                    <template v-slot:top>
                        <v-toolbar
                            :color="'teal-lighten-1'"
                            :rounded="true"
                            dark
                        >
                            <v-toolbar-title :style="'text-align: left'">{{ requestLogTableView }}</v-toolbar-title>

                            <v-spacer></v-spacer>

                        </v-toolbar>
                    </template>
                    </v-data-table>
                </v-col>
            </v-row>
        </v-form>
    </v-container>

</template>

<script>
import {personApi} from '../service/person.api.js'

export default {
    data() {
        return {
            //Table Headers
            CurrentTableHeaders1: [],
            CurrentTableHeaders2: [],

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
            accessLogTableView: "Access Logs",
            requestLogTableView: "Request Logs",
            selection: [],
            selectedRow: [],

            //DataBase Model Objects
            
            requestLogObj: {
                LogNum: "",
                ID: "",
                LockNum: "",
                AccessCode: "",
                DateRequested: "",
                DateApproved: "",
                Advisor: ""
            },
            accessLogObj: {
                LogNum: "",
                ID: "",
                LockNum: "",
                AccessTime: "",
                CID: "",
                Accepted: ""
            },

            //DataBase Return Lists
            accessLogList: [],
            requestLogList: [],


            showSelected: true,

        }
    },
    methods: {

        async GetAllAccessLogs() {
            await personApi.getAllAccessLogs(`${this.$route.params.id}`).then(response => {
                this.accessLogList = response;
                this.CurrentTableHeaders1 = this.AccessLogTableHeaders;
            });
        },

        async GetAllRequestLogs() {
            await personApi.getAllRequestLogs(this.$route.params.id).then(response => {
                this.requestLogList = response;
                this.CurrentTableHeaders2 = this.RequestLogTableHeaders;
            });
        },

        LoadTable(){
            this.GetAllAccessLogs();
            this.GetAllRequestLogs();
        },
        colorSelectedRow(row) {
            if(row.item === this.selectedRow[0]){
                return {style: 'background: #42A5F5; color: white;'};
            }
        },
        setAlertTimeOut(){
            setTimeout(() =>{
                this.successAlert=false
            }, 3000)
        }
    },
    watch: {
        selectedRow(){
            if(this.currentTableView === 'Access Logs'){
                if(this.selectedRow.length < 1){
                    this.accessLogObj = {
                        LogNum: "",
                        ID: "",
                        LockNum: "",
                        AccessTime: "",
                        CID: "",
                        Accepted: ""
                    }
                }
                else{
                    this.accessLogObj = this.selectedRow[0]
                }
            }
        }
    },
    async beforeMount() {
        this.LoadTable();
    },
    created(){

    }
    
}
</script>

<style scoped>

</style>