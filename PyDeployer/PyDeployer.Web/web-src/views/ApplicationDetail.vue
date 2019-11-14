<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">
                    Application Details
                </h1>
            </div>
        </section>
        <div class="columns">
            <div class="column">
                <application-details :application-id="applicationId" />
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <application-token-list :application-id="applicationId" />
            </div>
            <div class="column">
                <application-environment-list :application-id="applicationId" />
            </div>
        </div>
        <div class="columns">
            <div class="column">
                <application-environment-tokens :application-id="applicationId" />
            </div>
        </div>
    </div> 
</template>

<script>    
import ApplicationDetails from "@app/components/Application/ApplicationDetails.vue"
import ApplicationEnvironmentList from "@app/components/Application/ApplicationEnvironmentList.vue"
import ApplicationTokenList from "@app/components/Application/ApplicationTokenList.vue"
import ApplicationEnvironmentTokens from "@app/components/Application/ApplicationEnvironmentTokens.vue"

const applicationId = parseInt($("#applicationId").val(), 10);

export default {
    components: {
        ApplicationDetails,
        ApplicationEnvironmentList,
        ApplicationTokenList,
        ApplicationEnvironmentTokens
    },
    data: function() {
        return {
            applicationId: applicationId
        }
    }
}
</script>