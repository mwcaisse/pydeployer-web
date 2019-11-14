<template>
    <div>
        <div class="box">
            <div class="field">
                <label class="label">Name</label>
                <div class="control">
                    <span>{{name}}</span>
                </div>
            </div>
            <div class="field">
                <label class="label">UUID</label>
                <div class="control">
                    <span>{{uuid}}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import {ApplicationService} from "@app/services/ApplicationProxy.js"

export default {
    name: "application-details",
    data: function() {
        return {
            name: "",
            uuid: ""
        }
    },
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    methods: {
        fetchApplication: function () {
            ApplicationService.get(this.applicationId).then(function (data) {
                this.update(data);
            }.bind(this),
            function (error) {
                console.log("Error fetching applications: " + error)
            });

        },
        update: function (data) {
            this.name = data.name;
            this.uuid = data.applicationUuid;
        },
        clear: function () {
            this.name = "";
            this.uuid = "";
        }               
    },
    created: function () {
        this.fetchApplication();
    }
}
</script>

<style scoped>
    .action-icon:hover {
        font-size: 150%;
        cursor: pointer;
    }
</style>