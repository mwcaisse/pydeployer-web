<template>
    <div>      
        <user-authentication-token-list></user-authentication-token-list>  
    </div>
</template>

<script>
    import UserAuthenticationTokenList from "@app/components/User/AuthenticationTokenList.vue" 

    export default {
        data: function () {
            return {             
            }
        },
        components: {
            UserAuthenticationTokenList
        }
    }
</script>
