# Checkout Payment Gateway API

E-Commerce is experiencing exponential growth and merchants who sell their goods or services online need a way to easily collect money from their customers.
We would like to build a payment gateway on this project, an API based application that will allow a merchant to offer a way for their shoppers to pay for their product.

This REST API has two endpoint that to process a payment through the payment gateway and to retrieve details of a previously made paymen.Application also provides built-in documentations, tests and **Open API Specification** based API documentation.

## Installation

### Clone

Clone this repo to your local machine
### Run the app

    dotnet build
    docker-compose up

### Payment[Post]
Api provides a post action that makes a payment with given information. Card numbers can be used for testing purposes on this link https://docs.checkout.com/testing/test-card-numbers

#### Curl Request

    curl -X POST "https://localhost:32768/Payment" -H  "accept: */*" -H  "Content-Type: application/json-patch+json" -d "{\"card\":{\"card_number\":\"3530111333300000\",\"expiry_month\":10,\"expiry_year\":2023,\"cvv\":\"100\"},\"amount\":235,\"currency\":\"EUR\"}"

#### Request

    {
      "card": {
        "card_number": "3530111333300000",
        "expiry_month": 10,
        "expiry_year": 2023,
        "cvv": "100"
      },
      "amount": 235,
      "currency": "EUR"
    }

#### Response

    {
      "processId": "a97fd7ec-889e-4f58-8459-8cd2ca33f98d",
      "card_number": "************0000",
      "amount": 235,
      "currency": "EUR",
      "response_code": 10000,
      "response_summary": "Approved",
      "status": "Successful",
      "processed_at": "2020-11-01T21:58:50.196+00:00"
    }
    
### Payment[Get]
Merchants able to retrieve the details of a previously made payment with this api action

#### Curl Request

    curl -X GET "https://localhost:32768/Payment?id=a97fd7ec-889e-4f58-8459-8cd2ca33f98d" -H  "accept: */*"

#### Request

    /Payment?id=a97fd7ec-889e-4f58-8459-8cd2ca33f98d

#### Response

    {
      "processId": "a97fd7ec-889e-4f58-8459-8cd2ca33f98d",
      "card_number": "************0000",
      "amount": 235,
      "currency": "EUR",
      "response_code": 10000,
      "response_summary": "Approved",
      "status": "Successful",
      "processed_at": "2020-11-01T21:58:50.196"
    }

## Test
`xUnit` preferred for testing. `Moq`, `AutoFixture` and `FluentAssertions` also used testing environment. 

    cd PaymentGateway.Test/
    dotnet test .
      
## Notes

- Rate limiting would be a good practise
- Authentication ignored but its must on production
- Tests would be more elegant