<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">
                    Environment Details
                </h1>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <environment-details :environment-id="environmentId" />
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <environment-application-list :environment-id="environmentId" />
            </div>
            <div class="column">
                <database-list :environment-id="environmentId" />
            </div>
        </div>
    </div>
</template>

<script>
import EnvironmentDetails from "@app/components/Environment/EnvironmentDetails.vue"
import EnvironmentApplicationList from "@app/components/Environment/EnvironmentApplicationList.vue"
import DatabaseList from "@app/components/Database/DatabaseList.vue"

const environmentId = parseInt($("#environmentId").val(), 10);

export default {
    components: {
        EnvironmentDetails,
        EnvironmentApplicationList,
        DatabaseList
    },
    data: function () {
        return {
            environmentId: environmentId
        }
    }        
}
</script>

