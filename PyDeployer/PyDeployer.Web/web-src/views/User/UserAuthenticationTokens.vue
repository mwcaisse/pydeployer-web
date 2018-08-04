<template>
    <div>
        <section class="section">
            <div class="container">
                <h1 class="title">Authentication Tokens</h1>
            </div>
        </section>  
        <user-authentication-token-list></user-authentication-token-list>  
    </div>
</template>

<script>
    import UserAuthenticationTokenList from "components/User/AuthenticationTokenList.vue" 

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
