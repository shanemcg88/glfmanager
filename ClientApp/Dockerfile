# FROM node:14.17.4-alpine3.12 AS build-step
# WORKDIR /app
# COPY package.json ./
# RUN npm install
# COPY . .
# EXPOSE 4200
# CMD npm run start

FROM node:14.17
ARG WORK_DIR=/frontend
# to use 'ng' 
ENV PATH ${WORK_DIR}/node_modules/.bin:$PATH

RUN mkdir ${WORK_DIR}
WORKDIR ${WORK_DIR} 

COPY package.json ${WORK_DIR}
COPY package-lock.json ${WORK_DIR}

RUN npm install @angular/cli
RUN npm install

COPY . ${WORK_DIR}

EXPOSE 4200

# CMD ng serve --host 0.0.0.0
CMD npm start

# FROM nginx:1.20.1
# COPY --from=build /usr/local/app/test/client /usr/share/nginx/html
# EXPOSE 4200:80

# FROM nginx:1.20.1-alpine
# COPY nginx.conf /etc/nginx/nginx.conf
# COPY --from=build /usr/src/app/dist/client /usr/share/nginx/html

# FROM nginx AS runtime
# COPY --from=build /usr/src/app/dist/client /usr/share/nginx/html
