FROM mongo

COPY products.json /products.json 

CMD mongoimport --host catalogdb --db CatalogDB --collection Products --type json --file /products.json --jsonArray   
