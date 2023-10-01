import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning
import pandas as pd

# Disable SSL certificate verification warning (not recommended for production)
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

# url = "http://localhost:5286/uploadfinancialdata/sendbytes"
url = "http://146.59.35.43:8080/financialdata"

payload = """{
}"""
headers = {
  'Content-Type': 'application/json'
}

response = requests.request("POST", url, headers=headers, data=payload)

df = pd.read_json(response.text)
print(df)


aggregated_df = df.groupby("regon").agg({
    "year": "first",
    "plannedincome": "sum",
    "receivables": "sum",
    "madeincome": "sum",
    "plannedcost": "sum",
    "engagement": "sum",
    "madecost": "sum",
}).reset_index()

print(aggregated_df)
