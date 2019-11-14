<template>
    <div>
        <div class="box">
            <div class="field">
                <label class="label">Name</label>
                <div class="control">
                    <span>{{ name }}</span>
                </div>
            </div>
            <div class="field">
                <label class="label">UUID</label>
                <div class="control">
                    <span>{{ uuid }}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import {ApplicationService} from "@app/services/ApplicationProxy.js"

export default {
    name: "ApplicationDetails",
    props: {
        applicationId: {
            type: Number,
            required: true
        }
    },
    data: function() {
        return {
            name: "",
            uuid: ""
        }
    },
    created: function () {
        this.fetchApplication();
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
    }
}
</script>

<style scoped>
    .action-icon:hover {
        font-size: 150%;
        cursor: pointer;
    }
</style>