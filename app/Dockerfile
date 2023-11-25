FROM node:18-alpine

WORKDIR /sustainable-flow-app/

COPY public/ /sustainable-flow-app/public
COPY src/ /sustainable-flow-app/src
COPY package.json /sustainable-flow-app/

RUN npm install

CMD ["npm", "start"]