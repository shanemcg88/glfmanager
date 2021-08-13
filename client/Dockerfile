FROM node:14.17.4-alpine3.12 AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# FROM nginx:1.20.1
# COPY --from=build /usr/local/app/test/client /usr/share/nginx/html
# EXPOSE 4200:80

# FROM nginx:1.20.1-alpine
# COPY nginx.conf /etc/nginx/nginx.conf
# COPY --from=build /usr/src/app/dist/client /usr/share/nginx/html

FROM nginx AS runtime
COPY --from=build /usr/src/app/dist/client /usr/share/nginx/html
