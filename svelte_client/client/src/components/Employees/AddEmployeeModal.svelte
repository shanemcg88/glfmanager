<script>
  import {
    Button,
    Modal,
    ModalBody,
    ModalFooter,
    ModalHeader
  } from 'sveltestrap';

    let open = false;

    export const toggle = () => (open = !open);

</script>

<div>
    <Modal isOpen={open} backdrop="static" centered={true} size="xl" {toggle}>
      <ModalHeader {toggle}>Create Employee</ModalHeader>
      <ModalBody>
        <form>
            <div class="form-floating mb-3">
                <input type="text" class="form-control" id="firstName" placeholder="First name">
                <label for="firstName">First name</label>
              </div>
              <div class="form-floating">
                <input type="text" class="form-control" id="lastName" placeholder="Last name">
                <label for="lastName">Last name</label>
              </div>
        </form>
      </ModalBody>
      <ModalFooter>
        <Button color="primary" on:click={toggle}>Do Something</Button>
        <Button color="secondary" on:click={toggle}>Cancel</Button>
      </ModalFooter>
    </Modal>
</div>
