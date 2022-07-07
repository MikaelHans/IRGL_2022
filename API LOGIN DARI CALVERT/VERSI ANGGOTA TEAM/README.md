**NOTE:** sql e teko ojs.petra.ac.id Collation e tak ubah teko **utf8mb4_0900_ai_ci** jadi **utf8mb4_general_ci** ben iso jalan nang local ku, mungkin nek mau dilebokno server harus dibalikno maneh collation e

1. Import sql e sek ke database
2. Hit API login e nang

**http://localhost/<jenenge_folder_iki>/api/login.php**

**Request body gae testing:**
```json
{
	"email": "a@irgl.com"
	"password": "dummy"
}
```

---

**Responses**

**200, POST - Successfully logged in**

```json
{
	"success": true
	"message": "Successfully logged in"
}
```

**400, POST - Missing credentials**

```json
{
	"success": false
	"message": "Missing credentials"
}
```


**401, POST - Wrong credentials**

```json
{
	"success": false
	"message": "Wrong credentials"
}
```
